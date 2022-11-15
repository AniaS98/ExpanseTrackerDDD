using BaseDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events.IntegrationEvents
{
    public class AccountUpdatedEvent : IIntegrationEvent
    {
        public Account Account { get; private set; }
        public AccountUpdatedEvent(Account account)
        {
            this.Account = account;
        }
    }
}
