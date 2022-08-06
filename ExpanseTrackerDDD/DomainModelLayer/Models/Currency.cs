using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public enum CurrencyName
    {
        PLN,
        USD,
        EUR,
        GBP
    }

    public class Currency : ValueObject
    {
        public CurrencyName BaseCurrency { get; protected set; }
        public CurrencyName Name { get; protected set; }
        public decimal CurrentBuyValue { get; protected set; } //Buy, że user kupuje
        public decimal CurrentSellValue { get; protected set; } // Sell, że user sprzedaje
        public DateTime UpdateDateTime { get; protected set; } //2022-02-28
        //public Account Account { get; protected set; }
        public Guid AccountId { get; protected set; }

        private string Url = "https://cc-api.oanda.com/cc-api/v1/currencies?base={FROM}&quote={TO}&data_type=chart&start_date={STARTDATE}&end_date={ENDDATE}";
        public Currency(CurrencyName baseCurrency, CurrencyName name, Guid accountId)
        {
            this.BaseCurrency = baseCurrency;
            this.Name = name;
            this.UpdateDateTime = new DateTime();
            this.Url = Url.Replace("{FROM}", baseCurrency.ToString()).Replace("{TO}", Name.ToString());
            //this.Account = account;
            this.AccountId = accountId;
            UpdateCurrentValue(); //dopisać timeout 
        }

        //private Currency(string)

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }

        private async Task UpdateCurrentValue()
        {
            this.UpdateDateTime = DateTime.Now;
            // request po api zbierający obecną wartość
            this.Url = Url.Replace("{STARTDATE}", UpdateDateTime.AddDays(-1).ToString("yyyy-MM-dd"));
            this.Url = Url.Replace("{ENDDATE}", UpdateDateTime.ToString("yyyy-MM-dd"));
            HttpClient httpClient = new HttpClient();
            string result = "";
            try
            {
                result = await httpClient.GetStringAsync(Url);
            }
            catch (Exception e)
            {
                if (e.Message.Contains("400"))
                    Console.WriteLine("Currency not supported");
            }
            JObject json = JObject.Parse(result);
            this.CurrentBuyValue = Convert.ToDecimal(json.First.Value<JProperty>().Value.First["average_bid"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            this.CurrentSellValue = Convert.ToDecimal(json.First.Value<JProperty>().Value.First["average_ask"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
        }

        public void SetDefaultCurrency(CurrencyName name)
        {
            this.Url.Replace(this.Name.ToString(), name.ToString());
            this.Name = name;
            UpdateCurrentValue(); //dopisać timeout 
        }

        public decimal Buy(decimal value)
        {
            return value*this.CurrentBuyValue;
        }

        public decimal Sell(decimal value)
        {
            return value * this.CurrentSellValue;
        }

    }
}
