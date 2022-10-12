using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DTOs
{
    public enum RecurrencyTypeDto
    {
        None,
        Daily,
        Weekly,
        Monthly,
        Annual,
        Every_x_day
    }
    public enum RecurrencyPeriodDto
    {
        None,
        Number,
        Date,
        Endless
    }

    public class RecurrencyDto
    {
        public RecurrencyTypeDto Type { get; set; }
        public int DaysApart { get; set; }
        public int DayOfTheMonth { get; set; }
        public int NumberOfRecurrencies { get; set; }
        public int CurrentNumberOfRecurrencies { get; set; }
        public RecurrencyPeriodDto Period { get; set; }
        public DateTime EndDate { get; set; }
    }
}
