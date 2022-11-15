using BaseDDD.DomainModelLayer.Events;
using BaseDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.DomainModelLayer.Events.Implementations;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events
{
    public class TransactionUpdatedEvent : IDomainEvent
    {
        public Transaction Transaction { get; private set; }
        public Account Account { get; private set; }
        public Budget Budget { get; private set; }
        public Money Value { get; private set; }

        public TransactionUpdatedEvent(Transaction transaction, Account account, Budget budget, Money value)
        {
            this.Transaction = transaction;
            this.Account = account;
            this.Budget = budget;
            this.Value = value;
        }
    }
}

