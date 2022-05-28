using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public class Currency
    {
        public string Base { get; protected set; }
        public string Name { get; protected set; }
        public float CurrentValue { get; protected set; }
        public string Url { get; protected set; }
        public DateTime UpdateDateTime { get; protected set; }

        public Currency(string _base, string name)
        {
            this.Base = _base;
            this.Name = name;
            this.Url = "https://stooq.pl/q/?s=" + _base + name;
            UpdateCurrentValue(this.Url);
        }

        private void UpdateCurrentValue(string url)
        {
            // request po api zbierający obecną wartość

            UpdateDateTime = DateTime.Now;
        }

        public void UpdateCurrency(string name)
        {
            this.Url.Replace(this.Name, name);
            this.Name = name;
            UpdateCurrentValue(this.Url);
        }
        
    }
}
