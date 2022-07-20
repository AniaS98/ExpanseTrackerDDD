using System;
using System.Collections.Generic;
using System.Text;
using Base.DomainModelLayer.Events;
using Base.DomainModelLayer.Models;
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

    public class Transaction : AggregateRoot
    {
        public Guid Id { get; protected set; }
        public string Description { get; protected set; }
        public TransactionType Type { get; protected set; }
        public Money Value { get; protected set; }
        public Category TransactionCategory { get; protected set; }
        public Subcategory TransactionSubcategory { get; protected set; }
        public DateTime TransactionDate { get; protected set; }
        public TransactionStatus Status { get; protected set; }
        public TransactionFrequency Frequency { get; protected set; }
        public Recurrency TransactionRecurrency { get; protected set; }
        public string Note { get; protected set; }
        public string Contractor { get; protected set; }
        public Guid AccountId { get; protected set; }

        public Transaction(Guid id, IDomainEventPublisher domainEventPublisher, string description, TransactionType type, Money value, Category category, Subcategory subcategory, DateTime date, TransactionStatus status, Guid accountId, string contructor="", string note="") : base(id, domainEventPublisher)
        {
            this.Id = new Guid();
            this.Description = description;
            this.Type = type;
            this.Value = value;
            this.TransactionCategory = category;
            this.TransactionDate = date;
            this.Status = status;
            this.Frequency = TransactionFrequency.OneTime;
            this.TransactionRecurrency = TransactionRecurrency;
            this.AccountId = accountId;
        }




    }
}
