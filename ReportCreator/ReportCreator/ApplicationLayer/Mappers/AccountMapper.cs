using ReportCreator.ApplicationLayer.DTOs;
using ReportCreator.DomainModelLayer.Models;
using BaseDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.Mappers
{
    public class AccountMapper
    {
        public AccountDto Map(Account account)
        {
            return new AccountDto()
            {
                Currency = (CurrencyNameDto)account.Currency,
                Id = account.Id,
                Name = account.Name,
                OwnerId = account.OwnerId,
                AccountBalance = MoneyMapper.Map(account.AccountBalance),
                Overdraft = MoneyMapper.Map(account.Overdraft)
            };

        }
    }
}
