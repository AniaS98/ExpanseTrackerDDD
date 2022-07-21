using Base.DomainModelLayer.Models;
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
            CategoryDto result = new CategoryDto()
            {
                IconPath = category.IconPath,
                Name = category.Name,
                Subcategories = new List<SubcategoryDto>()
            };
            foreach (Subcategory subcategory in category.Subcategories)
            {
                SubcategoryDto subcategoryDto = Map(subcategory);
                result.Subcategories.Add(subcategoryDto);
            }
            return result;
        }

        public static SubcategoryDto Map(Subcategory subcategory)
        {
            return new SubcategoryDto()
            {
                IconPath = subcategory.IconPath,
                Name = subcategory.Name,
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
                Type = (RecurrencyTypeDto)recurrency.Type
            };
        }

        public static MoneyDto Map(Money money)
        {
            return new MoneyDto()
            {
                Amount = money.Amount,
                Currency = Map(money._Currency)
            };
        }

        public static CurrencyDto Map(Currency currency)
        {
            return new CurrencyDto()
            {
                Base = currency.Base,
                CurrentValue = currency.CurrentValue,
                httpClient = currency.httpClient,
                Name = (CurrencyNameDto)currency.Name,
                UpdateDateTime = currency.UpdateDateTime,
                Url = currency.Url
            };
        }

    }
}
