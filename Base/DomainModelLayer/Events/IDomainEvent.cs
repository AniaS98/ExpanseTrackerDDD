using Base.InfrastructureLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Base.DomainModelLayer.Events
{
    public interface IDomainEvent
    {
        List<IObserver> Observers { get; set; }
    }
}
