using System;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System.Diagnostics.CodeAnalysis;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.TransactionCommands
{
    public class CreateTransferCommand
    {
        public Guid FromTransactionId { get; set; }
        public Guid ToTransactionId { get; set; }
        public string Description { get; set; }
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
        public Guid FromAccountId { get; set; }
        public Guid ToAccountId { get; set; }
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
    }
}
