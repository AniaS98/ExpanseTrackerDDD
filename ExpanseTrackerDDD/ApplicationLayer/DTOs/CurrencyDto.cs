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
        public string Base { get; set; }
        public CurrencyNameDto Name { get; set; }
        public float CurrentValue { get; set; }
        public string Url { get; set; } //https://cc-api.oanda.com/cc-api/v1/currencies?base=PLN&quote=USD&data_type=chart&start_date=2022-05-28&end_date=2022-05-29
        public DateTime UpdateDateTime { get; set; } //2022-02-28
        public HttpClient httpClient { get; set; }
    }
}
