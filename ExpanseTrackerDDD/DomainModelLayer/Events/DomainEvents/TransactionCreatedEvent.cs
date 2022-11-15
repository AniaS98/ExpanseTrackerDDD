﻿using ExpanseTrackerDDD.InfrastructureLayer;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using ExpanseTrackerDDD.InfrastructureLayer.EF;
using ExpanseTrackerDDD.DomainModelLayer.Events.Implementations;
using BaseDDD.DomainModelLayer.Events;

namespace ExpanseTrackerDDD.DomainModelLayer.Events
{
    public class TransactionCreatedEvent: IDomainEvent
    {
        public Transaction Transaction { get; private set; }
        public Account Account { get; private set; }
        public Budget Budget { get; private set; }

        public TransactionCreatedEvent(Transaction transaction, Account account, Budget budget)
        {
            this.Transaction = transaction;
            this.Account = account;
            this.Budget = budget;
        }
    }
}
