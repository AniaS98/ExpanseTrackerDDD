using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Events.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Hosting.Internal;
using System.IO;
using ExpanseTrackerDDD.ApplicationLayer.DomainEventHandlers;

namespace ReportCreator
{
    public class Program
    {
        public static void Main(IServiceProvider serviceProvider)
        {
            var eventBus = serviceProvider.GetRequiredService<IEventBus>();
            eventBus.Subscribe<TransactionCreatedEvent>(TransactionCreatedEventHandler);


        }

    }
}
