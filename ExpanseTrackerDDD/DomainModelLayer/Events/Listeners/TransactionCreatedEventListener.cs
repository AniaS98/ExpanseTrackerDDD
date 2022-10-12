using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.InfrastructureLayer;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events.Listeners
{
    public class TransactionCreatedEventListener : IEventListener<TransactionCreatedEvent>
    {
        
        public void Handle(TransactionCreatedEvent eventData)
        {
            Console.WriteLine("Sending data to report bounded context");

            /*foreach(var observer in eventData.Observers)
            {
                observer.Update(this);
            }*/
        }
    }
}
