using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Events
{
    public interface IIntegrationEvent
    {
    }

    public interface IIntegrationEvent<TEvent> : IIntegrationEvent where TEvent : IIntegrationEvent
    {
        void Handle(TEvent eventData);
    }
}
