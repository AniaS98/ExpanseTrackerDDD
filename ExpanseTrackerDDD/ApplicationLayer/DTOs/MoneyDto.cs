using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DTOs
{
    public enum CurrencyNameDto
    {
        PLN,
        USD,
        EUR,
        GBP
    }

    public class MoneyDto
    {
        public decimal Amount { get; set; }
        public CurrencyNameDto Currency { get; set; }
    }
}
