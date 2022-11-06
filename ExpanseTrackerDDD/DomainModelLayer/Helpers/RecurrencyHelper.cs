using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Helpers
{
    public class RecurrencyHelper
    {
        //To chyba nie jest potrzebne
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
                        recurrency = new Recurrency(recurrencyType, 0, 1, recurrencyEndDate);
                        break;
                    }
                case 1:
                    {
                        if (numberOfRecurrencies == 0)
                        {
                            numberOfRecurrencies = (recurrencyEndDate.AddDays(1) - DateTime.Now).Days;
                        } 


                        int days = 1;
                        CaluculateNumberOfOccurenciesPerDaysApart(numberOfRecurrencies, recurrencyEndDate, days);
                        recurrency = new Recurrency(recurrencyType, days, numberOfRecurrencies, recurrencyEndDate);
                        break;
                    }
                case 2:
                    {
                        int days = 7;
                        CaluculateNumberOfOccurenciesPerDaysApart(numberOfRecurrencies, recurrencyEndDate, days);
                        recurrency = new Recurrency(recurrencyType, days, numberOfRecurrencies, recurrencyEndDate);
                        break;
                    }
                case 3:
                    {
                        if (numberOfRecurrencies == 0)
                        {
                            for (DateTime date = DateTime.Now; date <= recurrencyEndDate; date.AddMonths(1))
                            {
                                numberOfRecurrencies++;
                            }
                        }
                        recurrency = new Recurrency(recurrencyType, dayOfTheMonth, numberOfRecurrencies, recurrencyEndDate, 0);
                        break;
                    }
                case 4:
                    {
                        if (numberOfRecurrencies == 0)
                        {
                            for (DateTime date = DateTime.Now; date <= recurrencyEndDate; date.AddYears(1))
                            {
                                numberOfRecurrencies++;
                            }
                        }
                        recurrency = new Recurrency(recurrencyType, dayOfTheMonth, numberOfRecurrencies, recurrencyEndDate, 0);
                        break;
                    }
                case 5:
                    {
                        CaluculateNumberOfOccurenciesPerDaysApart(numberOfRecurrencies, recurrencyEndDate, daysApart);
                        recurrency = new Recurrency(recurrencyType, daysApart, numberOfRecurrencies, recurrencyEndDate);
                        break;
                    }
            }

            return recurrency;
        }

        public static int CaluculateNumberOfOccurenciesPerDaysApart(int numberOfRecurrencies, DateTime recurrencyEndDate, int days)
        {
            if (numberOfRecurrencies == 0)
            {
                for (DateTime date = DateTime.Now; date <= recurrencyEndDate; date.AddDays(days))
                {
                    numberOfRecurrencies++;
                }
            }
            return numberOfRecurrencies;
        }

    }
}
