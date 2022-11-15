using System;


namespace BaseDDD.DomainModelLayer.Models
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
