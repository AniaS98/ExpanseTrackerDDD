using BaseDDD.DomainModelLayer.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDDD.DomainModelLayer.Models
{
    public class EventBus
    {
        public ICollection<IEvent> IntegrationEvents;

        public EventBus()
        {
            IntegrationEvents = new List<IEvent>();
        }

        public void Add(IEvent @event)
        {
            IntegrationEvents.Add(@event);
        }
    }
}
