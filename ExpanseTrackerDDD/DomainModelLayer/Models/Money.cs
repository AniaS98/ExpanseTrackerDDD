using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Http;
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

    public enum Exchange
    {
        BUY,
        SELL
    }

    [Owned]
    public class Money : ValueObject
    {
        public decimal Amount { get; protected set; }
        public CurrencyName Currency { get; protected set; }

        private string Url = "https://cc-api.oanda.com/cc-api/v1/currencies?base={FROM}&quote={TO}&data_type=chart&start_date={STARTDATE}&end_date={ENDDATE}";

        public Money(decimal amount, CurrencyName currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        public async Task UpdateCurrentValue(CurrencyName from, CurrencyName to)
        {
            DateTime updateDateTime = DateTime.Now;
            // request po api zbierający obecną wartość
            Url = Url.Replace("{STARTDATE}", updateDateTime.AddDays(-1).ToString("yyyy-MM-dd"));
            Url = Url.Replace("{ENDDATE}", updateDateTime.ToString("yyyy-MM-dd"));
            Url = Url.Replace("{FROM}", from.ToString());
            Url = Url.Replace("{TO}", to.ToString());
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
            Amount *= Convert.ToDecimal(json.First.Value<JProperty>().Value.First["average_bid"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            /*if (exchange == Exchange.BUY)
                Amount *= Convert.ToDecimal(json.First.Value<JProperty>().Value.First["average_bid"].ToString(), System.Globalization.CultureInfo.InvariantCulture);
            else
                Amount *= Convert.ToDecimal(json.First.Value<JProperty>().Value.First["average_ask"].ToString(), System.Globalization.CultureInfo.InvariantCulture);*/
        }

        /*
        public async void SetDefaultCurrency(CurrencyName name)
        {
            this.Url.Replace(this.Name.ToString(), name.ToString());
            var task = UpdateCurrentValue();
            if (await Task.WhenAny(task, Task.Delay(6000)) == task)
            {
                Console.WriteLine("działa konwersja");
            }
        }*/

        public void Update(decimal amount)
        {
            this.Amount = amount;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
        public static Money operator +(Money a, Money b)
        {
            if (a.Currency == b.Currency)
                return new Money(a.Amount + b.Amount, a.Currency);
            else
                throw new Exception("Currencies do not match");
        }
        public static Money operator -(Money a, Money b)
        {
            if (a.Currency == b.Currency)
                return new Money(a.Amount - b.Amount, a.Currency);
            else
                throw new Exception("Currencies do not match");
        }
        public static Money operator /(Money a, Money b)
        {
            if (a.Currency == b.Currency)
                return new Money(a.Amount / b.Amount, a.Currency);
            else
                throw new Exception("Currencies do not match");
        }
        public static Money operator *(Money a, Money b)
        {
            if (a.Currency == b.Currency)
                return new Money(a.Amount * b.Amount, a.Currency);
            else
                throw new Exception("Currencies do not match");
        }
        public override string ToString()
        {
            return string.Format("{0}.2f {1}", Amount, Currency);
        }

    }

}
