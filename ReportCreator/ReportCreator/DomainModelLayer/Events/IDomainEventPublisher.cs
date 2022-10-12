using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Events
{
    public interface IDomainEventPublisher
    {
        void Publish<T>(T domainEvent) where T : IDomainEvent;
        void Subscribe<T>(IEventListener<T> listener) where T : IDomainEvent;
        void Unsubscribe<T>(IEventListener<T> listener) where T : IDomainEvent;
    }
}
