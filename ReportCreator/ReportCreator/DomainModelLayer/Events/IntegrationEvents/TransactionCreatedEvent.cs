using BaseDDD.DomainModelLayer.Events;
using ET_DML_M = ExpanseTrackerDDD.DomainModelLayer.Models;
using ET_DML_E = ExpanseTrackerDDD.DomainModelLayer.Events;
using RC_DML_M = ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Events.IntegrationEvents
{
    public class TransactionCreatedEvent : IDomainEvent
    {
        public RC_DML_M.Transaction Transaction { get; private set; }
        public RC_DML_M.Account Account { get; private set; }

        public TransactionCreatedEvent(RC_DML_M.Transaction transaction, RC_DML_M.Account account)
        {
            this.Transaction = transaction;
            this.Account = account;
        }

        public TransactionCreatedEvent(ET_DML_E.TransactionCreatedEvent eventData)
        {
            this.Transaction = new RC_DML_M.Transaction(eventData.Transaction, eventData.Budget.Id);
            this.Account = new RC_DML_M.Account(eventData.Account);
        }


        
    }
}
