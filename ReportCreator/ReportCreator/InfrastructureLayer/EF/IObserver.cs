using ReportCreator.DomainModelLayer.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.InfrastructureLayer.EF
{
    public interface IObserver
    {
        void Update<T>(IEventListener<T> listener) where T : IDomainEvent;
    }
}
