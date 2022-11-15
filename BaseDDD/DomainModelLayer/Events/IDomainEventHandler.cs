using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDDD.DomainModelLayer.Events
{
    public interface IDomainEventHandler<TEvent> : IEventHandler where TEvent : IDomainEvent
    {
        void Handle(TEvent eventData);

    }
}
