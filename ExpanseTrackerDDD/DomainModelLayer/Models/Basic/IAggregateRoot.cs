using System;


namespace ExpanseTrackerDDD.DomainModelLayer.Models.Basic
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
