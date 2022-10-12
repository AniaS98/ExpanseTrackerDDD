using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.TransactionCommands
{
    public class CreateTransactionCommand
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public CurrencyName Currency { get; set; }
        public CategoryName CatName { get; set; }
        public SubcategoryName CatSubcategoryName { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionStatus Status { get; set; }
        [AllowNull]
        public TransactionFrequency Frequency { get; set; }
        [AllowNull]
        public string Note { get; set; }
        [AllowNull]
        public string Contractor { get; set; }
        public Guid AccountId { get; set; }
        //Recurrency
        [AllowNull]
        public RecurrencyType RecurType { get; set; }
        [AllowNull]
        public RecurrencyPeriod Period { get; set; } = RecurrencyPeriod.None;
        [AllowNull]
        public int DaysApart { get; set; }
        [AllowNull]
        public int DayOfTheMonth { get; set; }
        [AllowNull]
        public int NumberOfRecurrencies { get; set; }
        [AllowNull]
        public int CurrentNumberOfRecurrencies { get; set; }
        [AllowNull]
        public DateTime EndDate { get; set; }
        [AllowNull]
        public Guid DestinationAccountId { get; set; }
        [AllowNull]
        public CurrencyName newCurrency { get; set; }
    }
}
