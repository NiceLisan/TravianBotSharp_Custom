﻿using MainCore.Tasks.Base;

namespace MainCore.Tasks
{
    [RegisterAsTransient(withoutInterface: true)]
    public class CompleteImmediatelyTask : VillageTask
    {
        public CompleteImmediatelyTask(IVillageRepository villageRepository) : base(villageRepository)
        {
        }

        protected override async Task<Result> Execute()
        {
            Result result;

            result = await new ToDorfCommand().Execute(_chromeBrowser, 0, false, CancellationToken);
            if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));

            result = await CompleteImmediately();
            if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));

            await new UpdateBuildingCommand().Execute(_chromeBrowser, AccountId, VillageId, CancellationToken);

            await _mediator.Publish(new CompleteImmediatelyMessage(AccountId, VillageId), CancellationToken);
            return Result.Ok();
        }

        protected override void SetName()
        {
            var villageName = _villageRepository.GetVillageName(VillageId);
            _name = $"Complete immediately in {villageName}";
        }

        public async Task<Result> CompleteImmediately()
        {
            var html = _chromeBrowser.Html;

            var completeNowButton = GetCompleteButton(html);
            if (completeNowButton is null) return Retry.ButtonNotFound("complete now");

            Result result;

            result = await _chromeBrowser.Click(By.XPath(completeNowButton.XPath));
            if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));

            bool confirmShown(IWebDriver driver)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(driver.PageSource);
                var confirmButton = GetConfirmButton(doc);
                return confirmButton is not null;
            };

            result = await _chromeBrowser.Wait(confirmShown, CancellationToken);
            if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));

            html = _chromeBrowser.Html;
            var confirmButton = GetConfirmButton(html);
            if (confirmButton is null) return Retry.ButtonNotFound("complete now");

            var oldQueueCount = new CountQueueBuilding().Execute(_chromeBrowser);

            result = await _chromeBrowser.Click(By.XPath(confirmButton.XPath));
            if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));

            bool queueDifferent(IWebDriver driver)
            {
                var doc = new HtmlDocument();
                doc.LoadHtml(driver.PageSource);
                var newQueueCount = new CountQueueBuilding().Execute(_chromeBrowser);
                return oldQueueCount != newQueueCount;
            };
            result = await _chromeBrowser.Wait(queueDifferent, CancellationToken);
            if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));

            return Result.Ok();
        }

        private static HtmlNode GetCompleteButton(HtmlDocument doc)
        {
            var finishClass = doc.DocumentNode
                .Descendants("div")
                .FirstOrDefault(x => x.HasClass("finishNow"));
            if (finishClass is null) return null;
            var button = finishClass
                .Descendants("button")
                .FirstOrDefault();
            return button;
        }

        private static HtmlNode GetConfirmButton(HtmlDocument doc)
        {
            var dialog = doc.GetElementbyId("finishNowDialog");
            if (dialog is null) return null;
            var button = dialog
                .Descendants("button")
                .FirstOrDefault();
            return button;
        }
    }
}