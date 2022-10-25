using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ExpanseTrackerDDD.DomainModelLayer.Factories
{
    public class TransactionFactory
    {
        

        public TransactionFactory()
        {
        }
        //Muszę jeszcze dodać opcje nasłuchiwania transakcji przez budżet żeby zmieniła się wartość obecnego wykorzystania limitu
        public Transaction CreateTransaction(Guid id, string description, TransactionType type, Money value, CategoryName categoryName, SubcategoryName categorySubcategoryName, RecurrencyType recurrencyType, int numberOfRecurrencies, DateTime recurrencyEndDate, RecurrencyPeriod period, int dayOfTheMonth, int daysApart, DateTime transactionDate, TransactionStatus status, Guid accountId, string contractor = "", string note = "")
        {
            Recurrency recurrency = new Recurrency();
            //int NumberOfRecurrencies
            //RecurrencyPeriod
            // EndDate

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

            Category category = new Category(categoryName, categorySubcategoryName);

            return new Transaction(id, description, type, value, category, recurrency, transactionDate, status, accountId, contractor, note);
        }

        public Transaction CreateTransfer(Transaction from, Guid destinationAccountId)
        {
            return new Transaction(new Guid(),from.Description, from.Type, from.Value, from.TransactionCategory, from.TransactionRecurrency, from.TransactionDate, from.Status, destinationAccountId);
        }

        public Transaction Exchange(Transaction from, Guid destinationAccountId, CurrencyName newCurrency)
        {
            Money value = from.Value;

            var task = Task.Run(async () => await value.UpdateCurrentValue(value.Currency, newCurrency));

            return new Transaction(new Guid(),  from.Description, from.Type, value, from.TransactionCategory, from.TransactionRecurrency, from.TransactionDate, from.Status, destinationAccountId);

        }



    }
}
