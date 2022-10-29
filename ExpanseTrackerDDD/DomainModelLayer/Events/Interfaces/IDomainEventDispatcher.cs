using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events.Interfaces
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<T>(T domainEvent) where T : IDomainEvent;
    }
}
