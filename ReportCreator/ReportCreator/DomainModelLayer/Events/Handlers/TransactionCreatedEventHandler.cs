using Base.DomainModelLayer.Events;
using Base.InfrastructureLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Events.Handlers
{
    public class TransactionCreatedEventHandler : IObserver
    {
        public void Update<T>(IEventListener<T> listener) where T : IDomainEvent
        {
            //Stworzyć transakcję za pomocą faktory, dodać do account
        }
    }
}
