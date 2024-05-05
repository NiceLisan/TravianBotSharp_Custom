﻿using MainCore.UI.Models.Output;

namespace MainCore.Repositories
{
    public interface IAccountRepository
    {
        bool Add(AccountDto dto);

        void Add(List<AccountDetailDto> dtos);

        void Delete(AccountId accountId);

        List<ListBoxItem> GetItems();

        void Update(AccountDto dto);
    }
}