using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models
{
    public class Money
    {
        public decimal Amount { get; protected set; }
        public string Name { get; protected set; }

        public Money(decimal amount, string name)
        {
            this.Amount = amount;
            this.Name = name;
        }

        public void Update(decimal amount)
        {
            this.Amount = amount;
        }

        public static Money operator +(Money a, Money b) => new Money(a.Amount + b.Amount, a.Name);


    }
}
