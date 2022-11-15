using BaseDDD.DomainModelLayer.Events;
using BaseDDD.DomainModelLayer.Events.Implementations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Events.Implementations
{
    public class SimpleEventDispatcher : IIntegrationEventDispatcher
    {
        protected IServiceProvider _serviceProvider;

        public SimpleEventDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Dispatch<T>(T integrationEvent) 
            where T : IIntegrationEvent
        {
            var _eventHandlers = _serviceProvider.GetServices<IEventHandler<IDomainEvent, IEvent>>();
            foreach (var handler in _eventHandlers)
            {
                handler.Handle(handler.Map(integrationEvent));
            }
        }
    }


}
