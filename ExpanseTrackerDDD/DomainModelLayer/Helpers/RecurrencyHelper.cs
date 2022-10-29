using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Helpers
{
    public class RecurrencyHelper
    {
        public static Recurrency SetRecurrency(RecurrencyType recurrencyType, int numberOfRecurrencies, DateTime recurrencyEndDate, RecurrencyPeriod period, int dayOfTheMonth, int daysApart)
        {
            Recurrency recurrency = new Recurrency();

            if (recurrencyType != RecurrencyType.None)
            {
                if (period == RecurrencyPeriod.None)
                    throw new Exception("Please specify until when should the transaction occur");
                if (period == RecurrencyPeriod.Number && recurrencyType == RecurrencyType.Every_x_day && String.IsNullOrEmpty(daysApart.ToString()))
                    throw new Exception("Please specify how many times should the transaction occur");
                if (period == RecurrencyPeriod.Date && String.IsNullOrEmpty(recurrencyEndDate.ToString()))
                    throw new Exception("Please specify until when should the transaction occur");
            }

            switch ((int)recurrencyType)
            {
                case 0:
                    {
                        break;
                    }
                case 1:
                    {
                        recurrency = new Recurrency(recurrencyType, 1, numberOfRecurrencies, recurrencyEndDate);
                        break;
                    }
                case 2:
                    {
                        recurrency = new Recurrency(recurrencyType, 7, numberOfRecurrencies, recurrencyEndDate);
                        break;
                    }
                case 3:
                    {
                        recurrency = new Recurrency(recurrencyType, dayOfTheMonth, numberOfRecurrencies, recurrencyEndDate, 0);
                        break;
                    }
                case 4:
                    {
                        recurrency = new Recurrency(recurrencyType, dayOfTheMonth, numberOfRecurrencies, recurrencyEndDate, 0);

                        break;
                    }
                case 5:
                    {
                        recurrency = new Recurrency(recurrencyType, daysApart, numberOfRecurrencies, recurrencyEndDate);
                        break;
                    }
            }

            return recurrency;
        }
    }
}
