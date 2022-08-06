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

        public static AccountDto Map(Account account)
        {
            AccountDto accountDto = new AccountDto()
            {
                AccountCurrency = Mappers.Map(account.AccountCurrency),
                AccountNumber = account.AccountNumber,
                Color = account.Color,
                Id = account.Id,
                Name = account.Name,
                Type = (AccountTypeDto)account.Type,
                UserId = account.UserId,
                TransactionDtos = new List<TransactionDto>()
            };
            foreach(Transaction t in account.Transactions)
            {
                accountDto.TransactionDtos.Add(TransactionMapper.Map(t));
            }
            return accountDto;
        }





    }
}
