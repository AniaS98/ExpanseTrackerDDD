using System;


namespace ExpanseTrackerDDD.DomainModelLayer.Interfaces
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
