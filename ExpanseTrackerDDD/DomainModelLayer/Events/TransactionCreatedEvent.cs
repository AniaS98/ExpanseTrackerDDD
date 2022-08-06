using ExpanseTrackerDDD.InfrastructureLayer;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events
{
    public class TransactionCreatedEvent : IDomainEvent
    {
        public string Description { get; protected set; }
        public TransactionType Type { get; protected set; }
        public Money Value { get; protected set; }
        public Category TransactionCategory { get; protected set; }
        public DateTime TransactionDate { get; protected set; }
        public TransactionStatus Status { get; protected set; }
        public TransactionFrequency Frequency { get; protected set; }
        public List<IObserver> Observers { get; set; }

        public TransactionCreatedEvent(string description, TransactionType type, Money value, Category transactionCategory, DateTime transactionDate, TransactionStatus status, TransactionFrequency frequency)
        {
            this.Description = description;
            this.Type = type;
            this.Value = value;
            this.TransactionCategory = transactionCategory;
            this.TransactionDate = transactionDate;
            this.Status = status;
            this.Frequency = frequency;
        }

    }
}
