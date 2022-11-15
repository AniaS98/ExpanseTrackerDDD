using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDDD.DomainModelLayer.Events
{
    public interface IIntegrationEventDispatcher
    {
        void Dispatch<T>(T integrationEvent) where T : IIntegrationEvent;
    }
}
