using BaseDDD.DomainModelLayer.Models;
using ReportCreator.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.Mappers
{
    public class MoneyMapper
    {
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
