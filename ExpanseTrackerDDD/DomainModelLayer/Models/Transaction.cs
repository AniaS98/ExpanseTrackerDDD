using System;
using System.Collections.Generic;
using System.Text;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public enum TransactionStatus
    {
        Upcomming,
        Planned,
        Owed
    }

    public enum TransactionFrequency
    {
        OneTime,
        Reoccuring
    }

    public enum TransactionType
    {
        Income,
        Expanse,
        Transfer,
        Exchange,
        Borrowed,
        Lent
    }

    public class Transaction : IAggregateRoot
    {
        public Guid Id { get; protected set; }
        public string Description { get; protected set; }
        public TransactionType Type { get; protected set; }
        public Money Balance { get; protected set; }
        public Category TransactionCategory { get; protected set; }
        public Subcategory TransactionSubcategory { get; protected set; }
        public DateTime TransactionDate { get; protected set; }
        public TransactionStatus Status { get; protected set; }
        public TransactionFrequency Frequency { get; protected set; }
        public Recurrency TransactionRecurrency { get; protected set; }
        public string Note { get; protected set; }
        public string Contractor { get; protected set; }

        public Transaction(string description, TransactionType type, decimal amount, Currency currency, Category category, Subcategory subcategory, DateTime date, TransactionStatus status, string contructor="", string note="")
        {
            this.Id = new Guid();
            this.Description = description;
            this.Type = type;
            this.Balance = new Money(amount, currency);
            this.TransactionCategory = category;
            this.TransactionDate = date;
            this.Status = status;
            this.Frequency = TransactionFrequency.OneTime;
            this.TransactionRecurrency = TransactionRecurrency;


        }




    }
}
