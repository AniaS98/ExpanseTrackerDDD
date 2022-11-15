using ET_DML_M = ExpanseTrackerDDD.DomainModelLayer.Models;
using ET_DML_E = ExpanseTrackerDDD.DomainModelLayer.Events;
using RC_DML_M = ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;
using BaseDDD.DomainModelLayer.Models;
using BaseDDD.DomainModelLayer.Events;

namespace ReportCreator.DomainModelLayer.Events.IntegrationEvents
{
    public class TransactionUpdatedEvent : IDomainEvent
    {
        public RC_DML_M.Transaction Transaction { get; private set; }
        public RC_DML_M.Account Account { get; private set; }
        public Money Value { get; private set; }

        public TransactionUpdatedEvent(RC_DML_M.Transaction transaction, RC_DML_M.Account account, Money value)
        {
            this.Transaction = transaction;
            this.Account = account;
            this.Value = value;
        }


    }
}
