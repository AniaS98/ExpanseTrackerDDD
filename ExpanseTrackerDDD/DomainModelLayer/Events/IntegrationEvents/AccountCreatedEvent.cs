using BaseDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events.IntegrationEvents
{
    public class AccountCreatedEvent : IIntegrationEvent
    {
        public Account Account { get; private set; }

        public AccountCreatedEvent(Account account)
        {
            this.Account = account;
        }
    }
}
