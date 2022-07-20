using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Base.DomainModelLayer.Models
{
    public enum CurrencyName
    {
        PLN,
        USD,
        EUR,
        GBP
    }

    public class Currency
    {
        public string Base { get; protected set; }
        public CurrencyName Name { get; protected set; }
        public float CurrentValue { get; protected set; }
        public string Url { get; protected set; } //https://cc-api.oanda.com/cc-api/v1/currencies?base=PLN&quote=USD&data_type=chart&start_date=2022-05-28&end_date=2022-05-29
        public DateTime UpdateDateTime { get; protected set; } //2022-02-28
        public HttpClient httpClient { get; protected set; }

        public Currency(string _base, CurrencyName name)
        {
            this.Base = _base;
            this.Name = name;
            this.UpdateDateTime = new DateTime();
            this.httpClient = new HttpClient();
            this.Url = "https://cc-api.oanda.com/cc-api/v1/currencies?base=" + this.Base+ "&quote="+ this.Name +"&data_type=chart&start_date={startdate}&end_date={enddate}";
            UpdateCurrentValue(this.Url);
            
            
        }

        private void UpdateCurrentValue(string url)
        {
            // request po api zbierający obecną wartość
            this.UpdateDateTime = DateTime.Now;
            this.Url = this.Url.Replace("{startdate}", this.UpdateDateTime.AddDays(-1).ToString("yyyy-MM-dd"));
            this.Url = this.Url.Replace("{enddate}", this.UpdateDateTime.ToString("yyyy-MM-dd"));
            httpClient.BaseAddress = new Uri(this.Url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            //dopisać
        }
        /*
        static async Task<string> GetProductAsync(string path)
        {
            HttpResponseMessage response = await httpClient.GetAsync(this.Url);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }*/



        public void SetDefaultCurrency(CurrencyName name)
        {
            this.Url.Replace(this.Name.ToString(), name.ToString());
            this.Name = name;
            UpdateCurrentValue(this.Url);
        }
        
    }
}
