using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Helpers;
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

        public Transaction CreateTransaction(Guid id, string description, TransactionType type, Money value, CategoryName categoryName, SubcategoryName categorySubcategoryName, RecurrencyType recurrencyType, int numberOfRecurrencies, DateTime recurrencyEndDate, RecurrencyPeriod period, int dayOfTheMonth, int daysApart, DateTime transactionDate, TransactionStatus status, Guid accountId, string contractor = "", string note = "")
        {
            Recurrency recurrency = RecurrencyHelper.SetRecurrency(recurrencyType, numberOfRecurrencies, recurrencyEndDate, period, dayOfTheMonth, daysApart);

            Category category = new Category(categoryName, categorySubcategoryName);

            return new Transaction(id, type, value, category, recurrency, transactionDate, status, accountId, description, contractor, note);
        }

        public Transaction CreateTransfer(Transaction from, Guid destinationAccountId)
        {
            return new Transaction(new Guid(), from.Type, from.Value, from.TransactionCategory, from.TransactionRecurrency, from.TransactionDate, from.Status, destinationAccountId, from.Description);
        }

        public Transaction Exchange(Transaction from, Guid destinationAccountId, CurrencyName newCurrency)
        {
            Money value = from.Value;

            var task = Task.Run(async () => await value.UpdateCurrentValue(value.Currency, newCurrency));

            return new Transaction(new Guid(), from.Type, value, from.TransactionCategory, from.TransactionRecurrency, from.TransactionDate, from.Status, destinationAccountId, from.Description);
        }

        public Transaction UpdateTransaction(Transaction transaction, string description, Money value, CategoryName categoryName, SubcategoryName categorySubcategoryName, RecurrencyType recurrencyType, int numberOfRecurrencies, DateTime recurrencyEndDate, RecurrencyPeriod period, int dayOfTheMonth, int daysApart, DateTime transactionDate, TransactionStatus status, Guid accountId, string contractor, string note)
        {
            Recurrency recurrency = RecurrencyHelper.SetRecurrency(recurrencyType, numberOfRecurrencies, recurrencyEndDate, period, dayOfTheMonth, daysApart);

            Category category = new Category(categoryName, categorySubcategoryName);

            transaction.UpdateTransaction(description, value, category, transactionDate, recurrency, status, accountId, contractor, note);

            return transaction;
        }
    }
}
