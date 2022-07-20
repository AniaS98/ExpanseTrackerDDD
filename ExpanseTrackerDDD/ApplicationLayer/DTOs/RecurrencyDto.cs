using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DTOs
{
    public enum RecurrencyTypeDto
    {
        Daily,
        Weekly,
        Monthly,
        Annual,
        Every_x_day
    }

    public class RecurrencyDto
    {
        public int DaysApart { get; set; }
        public int DayOfTheMonth { get; set; }
        public int NumberOfRecurrencies { get; set; }
        public DateTime EndDate { get; set; }
        public RecurrencyTypeDto Type { get; set; }
    }
}
