﻿using MainCore.Commands.UI.Village;
using MainCore.Entities;
using MainCore.Infrasturecture.AutoRegisterDi;
using MainCore.Repositories;
using MainCore.Services;
using MainCore.UI.Enums;
using MainCore.UI.Models.Output;
using MainCore.UI.Stores;
using MainCore.UI.ViewModels.Abstract;
using MainCore.UI.ViewModels.UserControls;
using MediatR;
using ReactiveUI;
using System.Reactive.Linq;
using Unit = System.Reactive.Unit;

namespace MainCore.UI.ViewModels.Tabs
{
    [RegisterAsSingleton(withoutInterface: true)]
    public class VillageViewModel : AccountTabViewModelBase
    {
        private readonly VillageTabStore _villageTabStore;

        private readonly ITaskManager _taskManager;
        private readonly IDialogService _dialogService;
        private readonly IUnitOfRepository _unitOfRepository;
        private readonly IMediator _mediator;
        public ListBoxItemViewModel Villages { get; } = new();

        public VillageTabStore VillageTabStore => _villageTabStore;
        public ReactiveCommand<Unit, Unit> LoadCurrent { get; }
        public ReactiveCommand<Unit, Unit> LoadUnload { get; }
        public ReactiveCommand<Unit, Unit> LoadAll { get; }
        public ReactiveCommand<AccountId, List<ListBoxItem>> LoadVillage { get; }

        public VillageViewModel(VillageTabStore villageTabStore, ITaskManager taskManager, IDialogService dialogService, IMediator mediator, IUnitOfRepository unitOfRepository)
        {
            _villageTabStore = villageTabStore;
            _dialogService = dialogService;

            _taskManager = taskManager;
            _mediator = mediator;
            _unitOfRepository = unitOfRepository;

            LoadCurrent = ReactiveCommand.CreateFromTask(LoadCurrentHandler);
            LoadUnload = ReactiveCommand.CreateFromTask(LoadUnloadHandler);
            LoadAll = ReactiveCommand.CreateFromTask(LoadAllHandler);
            LoadVillage = ReactiveCommand.CreateFromTask<AccountId, List<ListBoxItem>>(LoadVillageHandler);

            var villageObservable = this.WhenAnyValue(x => x.Villages.SelectedItem);
            villageObservable.BindTo(_selectedItemStore, vm => vm.Village);
            villageObservable.Subscribe(x =>
            {
                var tabType = VillageTabType.Normal;
                if (x is null) tabType = VillageTabType.NoVillage;
                _villageTabStore.SetTabType(tabType);
            });

            LoadVillage.Subscribe(villages => Villages.Load(villages));
        }

        public async Task VillageListRefresh(AccountId accountId)
        {
            if (!IsActive) return;
            if (accountId != AccountId) return;
            await LoadVillage.Execute(accountId);
        }

        protected override async Task Load(AccountId accountId)
        {
            await LoadVillage.Execute(accountId);
        }

        private async Task LoadCurrentHandler()
        {
            await _mediator.Send(new LoadCurrentCommand(AccountId, Villages));
        }

        private async Task LoadUnloadHandler()
        {
            await _mediator.Send(new LoadUnloadCommand(AccountId));
        }

        private async Task LoadAllHandler()
        {
            await _mediator.Send(new LoadAllCommand(AccountId));
        }

        private async Task<List<ListBoxItem>> LoadVillageHandler(AccountId accountId)
        {
            return await Task.Run(() => _unitOfRepository.VillageRepository.GetItems(accountId));
        }
    }
}