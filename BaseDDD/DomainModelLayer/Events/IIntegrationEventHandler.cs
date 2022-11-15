using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDDD.DomainModelLayer.Events
{

    public interface IIntegrationEventHandler<TEvent1, IIntegrationEvent> : IEventHandler where TEvent1 : IIntegrationEvent
    {
        void Handle(TEvent1 eventData);

        TEvent1 Map(IIntegrationEvent eventData);
    }
}
