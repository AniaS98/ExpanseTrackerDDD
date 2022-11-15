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

        public override string ToString()
        {
            return string.Format("{0} {1}", Math.Round(Amount, 2), Currency);
        }
    }
}
