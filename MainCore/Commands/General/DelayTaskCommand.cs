﻿using MainCore.Commands.Base;
using MainCore.Common.MediatR;

namespace MainCore.Commands.General
{
    public class DelayTaskCommand : ByAccountIdBase, ICommand
    {
        public DelayTaskCommand(AccountId accountId) : base(accountId)
        {
        }
    }

    [RegisterAsTransient]
    public class DelayTaskCommandHandler : ICommandHandler<DelayTaskCommand>
    {
        private readonly UnitOfRepository _unitOfRepository;

        public DelayTaskCommandHandler(UnitOfRepository unitOfRepository)
        {
            _unitOfRepository = unitOfRepository;
        }

        public async Task<Result> Handle(DelayTaskCommand command, CancellationToken cancellationToken)
        {
            var delay = _unitOfRepository.AccountSettingRepository.GetByName(command.AccountId, AccountSettingEnums.TaskDelayMin, AccountSettingEnums.TaskDelayMax);
            await Task.Delay(delay, CancellationToken.None);
            return Result.Ok();
        }
    }
}