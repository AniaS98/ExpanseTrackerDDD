using BaseDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events.IntegrationEvents
{
    public class UserLoggedInEvent : IIntegrationEvent
    {
        public User User { get; private set; }

        public UserLoggedInEvent(User user)
        {
            this.User = user;
        }
    }
}
