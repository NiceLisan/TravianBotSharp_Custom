﻿using FluentResults;
using HtmlAgilityPack;
using MainCore.Common.Errors;
using MainCore.Common.Models;
using MainCore.Infrasturecture.AutoRegisterDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chrome.ChromeDriverExtensions;
using OpenQA.Selenium.Support.UI;

namespace MainCore.Services
{
    [DoNotAutoRegister]
    public sealed class ChromeBrowser : IChromeBrowser
    {
        private ChromeDriver _driver;
        private readonly ChromeDriverService _chromeService;
        private WebDriverWait _wait;

        private readonly string[] _extensionsPath;
        private readonly HtmlDocument _htmlDoc = new();

        public ChromeBrowser(string[] extensionsPath)
        {
            _extensionsPath = extensionsPath;

            _chromeService = ChromeDriverService.CreateDefaultService();
            _chromeService.HideCommandPromptWindow = true;
        }

        public async Task<Result> Setup(ChromeSetting setting)
        {
            var options = new ChromeOptions();

            options.AddExtensions(_extensionsPath);

            if (!string.IsNullOrEmpty(setting.ProxyHost))
            {
                if (!string.IsNullOrEmpty(setting.ProxyUsername))
                {
                    options.AddHttpProxy(setting.ProxyHost, setting.ProxyPort, setting.ProxyUsername, setting.ProxyPassword);
                }
                else
                {
                    options.AddArgument($"--proxy-server={setting.ProxyHost}:{setting.ProxyPort}");
                }
            }

            options.AddArgument($"--user-agent={setting.UserAgent}");

            // So websites (Travian) can't detect the bot
            options.AddExcludedArgument("enable-automation");
            options.AddAdditionalOption("useAutomationExtension", false);
            options.AddArgument("--disable-blink-features=AutomationControlled");
            options.AddArgument("--disable-features=UserAgentClientHint");
            options.AddArgument("--disable-logging");
            options.AddArgument("--ignore-certificate-errors");

            options.AddArgument("--mute-audio");

            options.AddArguments("--no-default-browser-check", "--no-first-run");
            options.AddArguments("--no-sandbox", "--test-type");

            if (setting.IsHeadless)
            {
                //options.AddArgument("--remote-debugging-port=0");
                options.AddArgument("--headless=new");
            }
            else
            {
                options.AddArgument("--start-maximized");
            }

            var pathUserData = Path.Combine(AppContext.BaseDirectory, "Data", "Cache", setting.ProfilePath);
            if (!Directory.Exists(pathUserData)) Directory.CreateDirectory(pathUserData);

            pathUserData = Path.Combine(pathUserData, string.IsNullOrEmpty(setting.ProxyHost) ? "default" : setting.ProxyHost);

            options.AddArguments($"user-data-dir={pathUserData}");

            _driver = await Task.Run(() => new ChromeDriver(_chromeService, options));

            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(1);
            _wait = new WebDriverWait(_driver, TimeSpan.FromMinutes(3));
            return Result.Ok();
        }

        public ChromeDriver Driver => _driver;

        public HtmlDocument Html
        {
            get
            {
                UpdateHtml();
                return _htmlDoc;
            }
        }

        public async Task Shutdown()
        {
            if (_driver is null) return;

            try
            {
                await Task.Run(_driver.Quit);
            }
            catch { }
            _driver = null;
            _chromeService.Dispose();
        }

        public bool IsOpen()
        {
            try
            {
                _ = _driver.Title;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public string CurrentUrl => _driver.Url;

        public async Task<Result> Navigate(string url = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                return await Navigate(CurrentUrl);
            }

            Result goToUrl()
            {
                try
                {
                    _driver.Navigate().GoToUrl(url);
                    return Result.Ok();
                }
                catch (Exception exception)
                {
                    return Result.Fail(new Stop(exception.Message));
                }
            }
            var result = await Task.Run(goToUrl);
            if (result.IsFailed) return result.WithError(new TraceMessage(TraceMessage.Line()));
            result = await WaitPageLoaded();
            return result;
        }

        private void UpdateHtml(string source = null)
        {
            if (string.IsNullOrEmpty(source))
            {
                try
                {
                    _htmlDoc.LoadHtml(_driver.PageSource);
                }
                catch { }
            }
            else
            {
                _htmlDoc.LoadHtml(source);
            }
        }

        public async Task<Result> Click(By by)
        {
            var elements = _driver.FindElements(by);
            if (elements.Count == 0) return Retry.ElementNotFound();
            var element = elements[0];
            if (!element.Displayed || !element.Enabled) return Retry.ElementNotClickable();

            await Task.Run(element.Click);

            return Result.Ok();
        }

        public async Task<Result> InputTextbox(By by, string content)
        {
            var elements = _driver.FindElements(by);
            if (elements.Count == 0) return Retry.ElementNotFound();

            var element = elements[0];
            if (!element.Displayed || !element.Enabled) return Retry.ElementNotClickable();

            void input()
            {
                element.SendKeys(Keys.Home);
                element.SendKeys(Keys.Shift + Keys.End);
                element.SendKeys(content);
            }
            await Task.Run(input);

            return Result.Ok();
        }

        public async Task<Result> Wait(Func<IWebDriver, bool> condition)
        {
            Result wait()
            {
                try
                {
                    _wait.Until(condition);
                }
                catch (TimeoutException)
                {
                    return Result.Fail(new Stop("Page not loaded in 3 mins"));
                }
                return Result.Ok();
            }
            return await Task.Run(wait);
        }

        public async Task<Result> WaitPageLoaded()
        {
            static bool pageLoaded(IWebDriver driver) => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete");
            var result = await Wait(pageLoaded);
            return result;
        }

        public async Task<Result> WaitPageChanged(string part)
        {
            bool pageChanged(IWebDriver driver) => driver.Url.Contains(part);
            Result result;
            result = await Wait(pageChanged);
            if (result.IsFailed) return result;
            result = await WaitPageLoaded();
            return result;
        }

        public async Task Close() => await Task.Run(_driver.Close);
    }
}