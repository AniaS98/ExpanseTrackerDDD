using Base.DomainModelLayer.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.InfrastructureLayer
{
    public interface IObserver
    {
        void Update<T>(IEventListener<T> listener) where T : IDomainEvent;
    }
}