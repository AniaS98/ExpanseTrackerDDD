using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public enum RecurrencyType
    {
        None,
        Daily,
        Weekly,
        Monthly,
        Annual,
        Every_x_day 
    }

    public enum RecurrencyPeriod
    {
        None,
        Number,
        Date,
        Endless
    }

    public class Recurrency : ValueObject
    {
        public RecurrencyType Type { get; protected set; }
        public int DaysApart { get; protected set; }
        public int DayOfTheMonth { get; protected set; }
        public int NumberOfRecurrencies { get; protected set; } = 0;
        public int CurrentNumberOfRecurrencies { get; protected set; }
        public RecurrencyPeriod Period { get; protected set; }
        public DateTime EndDate { get; protected set; } = DateTime.Now;
        //public Guid TransactionId { get; protected set; }

        public Recurrency() { }

        public Recurrency(DateTime endDate) 
        { 
            this.EndDate = endDate;
            this.Type = RecurrencyType.None;
            this.DaysApart = 0;
            this.NumberOfRecurrencies = 0;
            this.EndDate = endDate;
        }

        public Recurrency(RecurrencyType type, int daysApart, int numberOfRecurrencies, DateTime endDate) 
        {
            this.Type = type;
            this.DaysApart = daysApart;
            this.NumberOfRecurrencies = numberOfRecurrencies;
            this.EndDate = endDate;
        }

        public Recurrency(RecurrencyType type, int dayOfTheMonth, int numberOfRecurrencies, DateTime endDate, int daysApart=0)
        {
            this.Type = type;
            this.DayOfTheMonth = dayOfTheMonth;
            this.NumberOfRecurrencies = numberOfRecurrencies;
            this.EndDate = endDate;
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

        /*public override string ToString()
        {
            StringBuilder sb = new StringBuilder();



            return base.ToString();
        }*/
    }
}
