using ExpanseTrackerDDD.DomainModelLayer.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events.Interfaces
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<TEvent> : IEventHandler where TEvent : IDomainEvent
    {
        void Handle(TEvent eventData);
    }

}
