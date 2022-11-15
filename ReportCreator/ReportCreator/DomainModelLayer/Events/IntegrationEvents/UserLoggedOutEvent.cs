using BaseDDD.DomainModelLayer.Events;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Events.IntegrationEvents
{
    public class UserLoggedOutEvent : IIntegrationEvent
    {
        public User User { get; private set; }

        public UserLoggedOutEvent(User user)
        {
            this.User = user;
        }
    }
}
