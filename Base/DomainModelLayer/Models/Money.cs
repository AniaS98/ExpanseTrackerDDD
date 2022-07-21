using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DomainModelLayer.Models
{
    public class Money : ValueObject
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
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
        public static Money operator +(Money a, Money b)
        {
            if (a._Currency == b._Currency)
                return new Money(a.Amount + b.Amount, a._Currency);
            else
                throw new Exception("Currencies do not match");
        }
        public static Money operator -(Money a, Money b)
        {
            if (a._Currency == b._Currency)
                return new Money(a.Amount - b.Amount, a._Currency);
            else
                throw new Exception("Currencies do not match");
        }
        public static Money operator /(Money a, Money b)
        {
            if (a._Currency == b._Currency)
                return new Money(a.Amount / b.Amount, a._Currency);
            else
                throw new Exception("Currencies do not match");
        }
        public static Money operator *(Money a, Money b)
        {
            if (a._Currency == b._Currency)
                return new Money(a.Amount * b.Amount, a._Currency);
            else
                throw new Exception("Currencies do not match");
        }
        public override string ToString()
        {
            return string.Format("{0}.2f {1}", Amount, _Currency);
        }

    }
}
