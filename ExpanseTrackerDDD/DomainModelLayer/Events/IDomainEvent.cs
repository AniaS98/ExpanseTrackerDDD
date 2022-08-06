using ExpanseTrackerDDD.InfrastructureLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Events
{
    public interface IDomainEvent
    {
        List<IObserver> Observers { get; set; }
    }
}
