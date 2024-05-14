﻿namespace MainCore.Commands.Features.DisableRedRaidReport
{
    public class DisableRedRaidCommand
    {
        public async Task<Result> Execute(IChromeBrowser chromeBrowser, CancellationToken cancellationToken)
        {
            var html = chromeBrowser.Html;
            Result result;

            do
            {
                var newReport = GetNewReport(html);
                if (newReport is null) return Result.Ok();

                var currentUrl = chromeBrowser.CurrentUrl;
                result = await chromeBrowser.Click(By.XPath(newReport.XPath));
                if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));

                result = await chromeBrowser.WaitPageLoaded(cancellationToken);
                if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));

                result = await OpenRaidList(chromeBrowser, cancellationToken);
                if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));
                result = await Deactive(chromeBrowser);
                if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));
                result = await Save(chromeBrowser);
                if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));

                result = await chromeBrowser.Navigate(currentUrl, cancellationToken);
                if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));
            } while (true);
        }

        private static HtmlNode GetNewReport(HtmlDocument doc)
        {
            var overview = doc.GetElementbyId("overview");

            var td = overview.Descendants("td")
                .FirstOrDefault(x => x.HasClass("sub") && x.HasClass("newMessage"));
            if (td is null) return null;
            var a = td.Descendants("a").LastOrDefault();
            if (a is null) return null;
            return a;
        }

        private static async Task<Result> OpenRaidList(IChromeBrowser chromeBrowser, CancellationToken cancellationToken)
        {
            var html = chromeBrowser.Html;
            var raidListGoldclub = html.GetElementbyId("raidListGoldclub");

            Result result;
            result = await chromeBrowser.Click(By.XPath(raidListGoldclub.XPath));
            if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));
            static bool farmListDiaglog(IWebDriver driver)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(driver.PageSource);

                var farmListTargetForm = doc.GetElementbyId("farmListTargetForm");
                if (farmListTargetForm is null) return false;

                return !doc.DocumentNode.Descendants("div").Any(x => x.HasClass("contentLoadingIndicator"));
            }

            result = await chromeBrowser.Wait(farmListDiaglog, cancellationToken);
            if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));
            return Result.Ok();
        }

        private static async Task<Result> Deactive(IChromeBrowser chromeBrowser)
        {
            var html = chromeBrowser.Html;
            var farmListTargetForm = html.GetElementbyId("farmListTargetForm");
            var activeInput = farmListTargetForm.Descendants("input").FirstOrDefault(x => x.GetAttributeValue("name", "") == "isActive");
            if (activeInput is null) return Retry.NotFound("active farm", "check box ");
            if (activeInput.GetAttributeValue("value", "") == "false") return Result.Ok();

            Result result;
            result = await chromeBrowser.Click(By.XPath(activeInput.XPath));
            if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));
            return Result.Ok();
        }

        private static async Task<Result> Save(IChromeBrowser chromeBrowser)
        {
            var html = chromeBrowser.Html;
            var farmListTargetForm = html.GetElementbyId("farmListTargetForm");
            var saveButton = farmListTargetForm.Descendants("button").FirstOrDefault(x => x.HasClass("save"));

            if (saveButton is null) return Retry.ButtonNotFound("save farmlist");

            Result result;
            result = await chromeBrowser.Click(By.XPath(saveButton.XPath));
            if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));
            return Result.Ok();
        }
    }
}