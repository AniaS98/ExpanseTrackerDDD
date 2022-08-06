using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Mappers
{
    public class Mappers
    {
        public static CategoryDto Map(Category category)
        {
            return new CategoryDto()
            {
                IconPath = category.IconPath,
                Name = category.Name,
                TransactionDtoId = category.TransactionId
            };
        }

        public static RecurrencyDto Map(Recurrency recurrency)
        {
            return new RecurrencyDto()
            {
                DayOfTheMonth = recurrency.DayOfTheMonth,
                DaysApart = recurrency.DaysApart,
                EndDate = recurrency.EndDate,
                NumberOfRecurrencies = recurrency.NumberOfRecurrencies,
                Type = (RecurrencyTypeDto)recurrency.Type,
                TransactionId = recurrency.TransactionId
            };
        }

        public static MoneyDto Map(Money money)
        {
            return new MoneyDto()
            {
                Amount = money.Amount,
                Currency = Map(money._Currency),
                ForeignKey = money.ForeignKey
            };
        }

        public static CurrencyDto Map(Currency currency)
        {
            return new CurrencyDto()
            {
                Base = (CurrencyNameDto)currency.BaseCurrency,
                CurrentBuyValue = currency.CurrentBuyValue,
                CurrentSellValue = currency.CurrentSellValue,
                Name = (CurrencyNameDto)currency.Name,
                UpdateDateTime = currency.UpdateDateTime,
                AccountId = currency.AccountId
            };
        }

    }
}
