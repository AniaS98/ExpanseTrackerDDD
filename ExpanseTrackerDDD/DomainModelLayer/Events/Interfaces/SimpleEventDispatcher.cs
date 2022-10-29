using ExpanseTrackerDDD.DomainModelLayer.Events.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events
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
            var _eventHandlers = _serviceProvider.GetServices<IEventHandler<T>>();
            foreach (var handler in _eventHandlers)
            {
                handler.Handle(domainEvent);
            }
        }



    }
}
