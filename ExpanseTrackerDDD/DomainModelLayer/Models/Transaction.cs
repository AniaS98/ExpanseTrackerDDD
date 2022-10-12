using System;
using System.Collections.Generic;
using System.Text;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public enum TransactionStatus
    {
        Settled,
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
        public string Description { get; protected set; }
        public TransactionType Type { get; protected set; }
        public Money Value { get; protected set; }
        public Category TransactionCategory { get; protected set; }
        public DateTime TransactionDate { get; protected set; }
        public TransactionStatus Status { get; protected set; }
        public TransactionFrequency Frequency { get; protected set; }
        public Recurrency TransactionRecurrency { get; protected set; }
        public string Note { get; protected set; }
        public string Contractor { get; protected set; }
        public Guid AccountId { get; protected set; }

        public Transaction(Guid id, IDomainEventPublisher domainEventPublisher, string description, TransactionType type, Money value, Category category, Recurrency recurrency, DateTime transactionDate, TransactionStatus status, Guid accountId, string contractor = "", string note="") : base(id, domainEventPublisher)
        {
            this.Id = id;
            this.Description = description;
            this.Type = type;
            this.Value = value;
            this.TransactionCategory = category;
            this.TransactionDate = transactionDate;
            this.Status = status;
            this.Frequency = TransactionFrequency.OneTime;
            this.TransactionRecurrency = recurrency;
            this.AccountId = accountId;
            this.Contractor = contractor;
            this.Note = note;
        }

        public void UpdateTransaction(string description, TransactionType type, Money value, Category category, DateTime transactionDate, TransactionStatus status, Guid accountId, string contractor, string note)
        {
            this.Description = description;
            this.Type = type;
            this.Value = value;
            this.TransactionCategory = category;
            this.TransactionDate = transactionDate;
            this.Status = status;
            this.Frequency = TransactionFrequency.OneTime;
            this.TransactionRecurrency = TransactionRecurrency;
            this.AccountId = accountId;
            this.Contractor = contractor;
            this.Note = note;
        }





    }
}
