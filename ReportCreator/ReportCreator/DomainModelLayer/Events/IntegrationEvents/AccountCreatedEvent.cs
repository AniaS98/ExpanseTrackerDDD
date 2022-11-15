using BaseDDD.DomainModelLayer.Events;
using RC_DML_M = ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Events.IntegrationEvents
{
    public class AccountCreatedEvent : IIntegrationEvent
    {
        public RC_DML_M.Account Account { get; private set; }

        public AccountCreatedEvent(RC_DML_M.Account account)
        {
            this.Account = account;
        }

    }
}
