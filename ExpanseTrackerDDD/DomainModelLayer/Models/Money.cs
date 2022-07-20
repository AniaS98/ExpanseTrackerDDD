using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public class Money
    {
        public decimal Amount { get; protected set; }
        public Currency _Currency { get; protected set; }

        public Money(decimal amount, Currency currency)
        {
            this.Amount = amount;
            this._Currency = currency;
        }

        public void Update(decimal amount)
        {
            this.Amount = amount;
        }


    }
}
