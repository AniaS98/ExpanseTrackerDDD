using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDDD.DomainModelLayer.Events
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<IDomainEvent, IEvent> : IEventHandler
    {
        void Handle(IDomainEvent eventData);
        IDomainEvent Map(IEvent eventData);
    }
}
