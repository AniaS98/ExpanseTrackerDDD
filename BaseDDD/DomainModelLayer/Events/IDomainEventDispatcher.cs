using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDDD.DomainModelLayer.Events
{
    public interface IDomainEventDispatcher
    {
        void Dispatch<T>(T domainEvent) where T : IDomainEvent;
    }
}
