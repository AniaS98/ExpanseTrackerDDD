using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using Newtonsoft.Json.Linq;
using BaseDDD.DomainModelLayer.Models;

namespace ExpanseTrackerDDD.ApplicationLayer.Services
{
    public class Services
    {

        public async Task UpdateCurrentValue(Money value, CurrencyName to)
        {

            string url = "http://api.nbp.pl/api/exchangerates/rates/{table}/{code}/";
            url = url.Replace("{table}", "A");

            url = url.Replace("{code}", to.ToString());

            decimal amount = 0.00m;
            string result = "";

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    result = responseContent.ReadAsStringAsync().GetAwaiter().GetResult();

                }
                else
                    Console.WriteLine(String.Format("{0} is not available in the NBP portal"));

            }
            if (result != "")
            {
                amount = Convert.ToDecimal(JObject.Parse(result).GetValue("rates").First["mid"].ToString().Replace(",", "."));
            }
            else
                throw new Exception("Unable to find exchange rate for the provided currency");
            
            value = new Money(amount, to);
        }



    }
}
