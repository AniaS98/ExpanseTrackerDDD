using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public enum RecurrencyType
    {
        Daily,
        Weekly,
        Monthly,
        Annual,
        Every_x_day 
    }

    public class Recurrency : ValueObject
    {
        public int DaysApart { get; protected set; }
        public int DayOfTheMonth { get; protected set; }
        public int NumberOfRecurrencies { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public RecurrencyType Type { get; protected set; }
        public Guid TransactionId { get; protected set; }
        //public Transaction Transaction { get; protected set; }

        public Recurrency(RecurrencyType type, int numberOfRecurrencies, Guid transactionId)
        {
            this.NumberOfRecurrencies = numberOfRecurrencies;
            this.Type = type;
            this.TransactionId = transactionId;
            //this.Transaction = transaction;
        }

        public Recurrency(RecurrencyType type, int numberOfRecurrencies, Guid transactionId, int dayOfTheMonth)
        {
            this.NumberOfRecurrencies = numberOfRecurrencies;
            this.Type = type;
            this.TransactionId = transactionId;
            //this.Transaction = transaction;
        }

        public void SetDaysApart_DayOfTheMonth(RecurrencyType type, int dayOfTheMonth = 0)
        {
            switch (type)
            {
                case RecurrencyType.Daily:
                    {
                        this.DaysApart = 1;
                        break;
                    }
                case RecurrencyType.Weekly:
                    {
                        this.DaysApart = 7;
                        break;
                    }
                case RecurrencyType.Monthly:
                    {
                        this.DayOfTheMonth = dayOfTheMonth;
                        this.DaysApart = 27;
                        break;
                    }
                case RecurrencyType.Annual:
                    {
                        this.DayOfTheMonth = dayOfTheMonth;
                        this.DaysApart = 360;
                        break;
                    }
                case RecurrencyType.Every_x_day:
                    {
                        this.DaysApart = dayOfTheMonth;
                        break;
                    }
            }
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
