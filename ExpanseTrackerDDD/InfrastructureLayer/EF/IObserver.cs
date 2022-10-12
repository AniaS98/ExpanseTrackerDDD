using ExpanseTrackerDDD.DomainModelLayer.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.InfrastructureLayer.EF
{
    public interface IObserver
    {
        void Update<T>(IEventListener<T> listener) where T : IDomainEvent;
    }
}