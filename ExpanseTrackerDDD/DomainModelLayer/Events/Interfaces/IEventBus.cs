using ExpanseTrackerDDD.DomainModelLayer.Events.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events.Interfaces
{
    public interface IEventBus
    {
        void Publish<T>(T integrationEvent) where T : IIntegrationEvent;

        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        void Unsubscribe<T>(IIntegrationEventHandler<T> handler)
            where T : IntegrationEvent;

    }
}
