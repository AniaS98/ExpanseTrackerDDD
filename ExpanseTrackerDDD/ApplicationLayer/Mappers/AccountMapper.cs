using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Mappers
{
    public class AccountMapper
    {
        public List<AccountDto> Map(IList<Account> accounts)
        {
            List<AccountDto> result = new List<AccountDto>();
            foreach (Account account in accounts)
            {
                AccountDto accountDto = Map(account);
                result.Add(accountDto);
            }

            return result;
        }

        public AccountDto Map(Account account)
        {
            AccountDto accountDto = new AccountDto()
            {
                Currency = (CurrencyNameDto)account.CurrencyName,
                AccountNumber = account.AccountNumber,
                Id = account.Id,
                Name = account.Name,
                Type = (AccountTypeDto)account.Type,
                UserId = account.UserId,
            };
            accountDto.Balance = Mappers.Map(account.Balance);
            accountDto.Overdraft = Mappers.Map(account.Overdraft);

            return accountDto;
        }

    }
}
