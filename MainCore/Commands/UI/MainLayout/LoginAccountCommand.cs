﻿using FluentResults;
using MainCore.Commands.General;
using MainCore.Common.Enums;
using MainCore.Common.Errors;
using MainCore.Common.MediatR;
using MainCore.Entities;
using MainCore.Notification.Message;
using MainCore.Services;
using MediatR;

namespace MainCore.Commands.UI.MainLayout
{
    public class LoginAccountCommand : ByAccountIdBase, IRequest<Result>
    {
        public LoginAccountCommand(AccountId accountId) : base(accountId)
        {
        }
    }

    public class LoginAccountCommandHandler : IRequestHandler<LoginAccountCommand, Result>
    {
        private readonly ITaskManager _taskManager;
        private readonly ITimerManager _timerManager;

        private readonly IChooseAccessCommand _chooseAccessCommand;
        private readonly IOpenBrowserCommand _openBrowserCommand;
        private readonly ILogService _logService;
        private readonly IMediator _mediator;

        public LoginAccountCommandHandler(ITaskManager taskManager, ITimerManager timerManager, IOpenBrowserCommand openBrowserCommand, IChooseAccessCommand chooseAccessCommand, ILogService logService, IMediator mediator)
        {
            _taskManager = taskManager;
            _timerManager = timerManager;
            _openBrowserCommand = openBrowserCommand;
            _chooseAccessCommand = chooseAccessCommand;
            _logService = logService;
            _mediator = mediator;
        }

        public async Task<Result> Handle(LoginAccountCommand request, CancellationToken cancellationToken)
        {
            var accountId = request.AccountId;
            _taskManager.SetStatus(accountId, StatusEnums.Starting);

            Result result;
            result = await _chooseAccessCommand.Execute(accountId, true);

            if (result.IsFailed) return result.WithError(new TraceMessage(TraceMessage.Line()));
            var logger = _logService.GetLogger(accountId);
            var access = _chooseAccessCommand.Value;
            logger.Information("Using connection {proxy} to start chrome", access.Proxy);
            result = await _openBrowserCommand.Execute(accountId, access);
            if (result.IsFailed) return result.WithError(new TraceMessage(TraceMessage.Line()));

            await _mediator.Publish(new AccountInit(accountId));

            _timerManager.Start(accountId);
            _taskManager.SetStatus(accountId, StatusEnums.Online);
            return Result.Ok();
        }
    }
}