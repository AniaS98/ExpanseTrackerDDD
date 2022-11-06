using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Events.Interfaces
{
    public interface IIntegrationEventDispatcher
    {
        void Dispatch<T>(T domainEvent) where T : IIntegrationEvent;
    }
}
