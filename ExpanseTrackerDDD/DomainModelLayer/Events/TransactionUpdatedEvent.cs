using ExpanseTrackerDDD.DomainModelLayer.Events.Implementations;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events
{
    public class TransactionUpdatedEvent : DomainEvent
    {
        public Transaction Transaction { get; private set; }
        public Account Account { get; private set; }
        public Money Value { get; private set; }

        public TransactionUpdatedEvent(Transaction transaction, Account account, Money value)
        {
            this.Transaction = transaction;
            this.Account = account;
            this.Value = value;
        }
    }
}

