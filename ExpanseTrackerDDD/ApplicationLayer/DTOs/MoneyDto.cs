using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DTOs
{
    public class MoneyDto
    {
        public decimal Amount { get; set; }
        public CurrencyDto Currency { get; set; }
        public Guid ForeignKey { get; set; }
    }
}
