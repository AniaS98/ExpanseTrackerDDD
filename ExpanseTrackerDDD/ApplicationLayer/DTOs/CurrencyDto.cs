using System;
using System.Collections.Generic;
using System.Net.Http;
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
    public class CurrencyDto
    {
        public CurrencyNameDto Base { get; set; }
        public CurrencyNameDto Name { get; set; }
        public decimal CurrentBuyValue { get; set; }
        public decimal CurrentSellValue { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public Guid AccountId { get; set; }
    }
}
