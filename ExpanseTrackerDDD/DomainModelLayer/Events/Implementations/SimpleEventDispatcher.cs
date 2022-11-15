using BaseDDD.DomainModelLayer.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events.Implementations
{
    public class SimpleEventDispatcher : IDomainEventDispatcher
    {
        protected IServiceProvider _serviceProvider;

        public SimpleEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<T>(T domainEvent)
            where T : IDomainEvent
        {
            var _eventHandlers = _serviceProvider.GetServices<IDomainEventHandler<T>>();
            foreach (var handler in _eventHandlers)
            {
                handler.Handle(domainEvent);
            }
        }



    }
}
