using BaseDDD.DomainModelLayer.Models;
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
                Name = (CategoryNameDto)category.Name,
                SubcategoryName = (SubcategoryNameDto)category.SubcategoryName
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
                CurrentNumberOfRecurrencies = recurrency.CurrentNumberOfRecurrencies,
                Period = (RecurrencyPeriodDto)recurrency.Period
            };
        }

        public static MoneyDto Map(Money money)
        {
            return new MoneyDto()
            {
                Amount = money.Amount,
                Currency = (CurrencyNameDto)(money.Currency),
            };
        }

    }
}
