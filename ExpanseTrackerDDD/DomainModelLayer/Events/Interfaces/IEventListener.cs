using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events.Interfaces
{
    public interface IEventListener
    { }

    public interface IEventListener<TEvent> : IEventListener where TEvent : IIntegrationEvent
    {
        void Handle(TEvent eventData);
    }
}
