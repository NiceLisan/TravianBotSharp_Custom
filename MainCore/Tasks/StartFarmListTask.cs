﻿using MainCore.Commands.Features.StartFarmList;
using MainCore.Tasks.Base;

namespace MainCore.Tasks
{
    [RegisterAsTransient(withoutInterface: true)]
    public class StartFarmListTask : AccountTask
    {
        private readonly ITaskManager _taskManager;

        public StartFarmListTask(IMediator mediator, ITaskManager taskManager) : base(mediator)
        {
            _taskManager = taskManager;
        }

        protected override async Task<Result> Execute()
        {
            Result result;

            result = await new ToFarmListPageCommand().Execute(_chromeBrowser, AccountId, CancellationToken);
            if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));

            var useStartAllButton = new GetAccountSetting().BooleanByName(AccountId, AccountSettingEnums.UseStartAllButton);
            if (useStartAllButton)
            {
                result = await new StartAllFarmListCommand().Execute(_chromeBrowser);
                if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));
            }
            else
            {
                result = await new StartActiveFarmListCommand().Execute(_chromeBrowser, AccountId);
                if (result.IsFailed) return result.WithError(TraceMessage.Error(TraceMessage.Line()));
            }
            await SetNextExecute();
            return Result.Ok();
        }

        private async Task SetNextExecute()
        {
            var seconds = new GetAccountSetting().ByName(AccountId, AccountSettingEnums.FarmIntervalMin, AccountSettingEnums.FarmIntervalMax);
            ExecuteAt = DateTime.Now.AddSeconds(seconds);
            await _taskManager.ReOrder(AccountId);
        }

        protected override void SetName()
        {
            _name = "Start farm list";
        }
    }
}