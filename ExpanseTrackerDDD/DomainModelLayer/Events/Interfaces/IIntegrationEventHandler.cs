using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events.Interfaces
{
    public interface IIntegrationEventHandler
    {
        void Add<T>(IIntegrationEventHandler<T> handler) where T : IIntegrationEvent;
    }

    public interface IIntegrationEventHandler<TEvent> : IIntegrationEventHandler where TEvent : IIntegrationEvent
    {
    }
}

