using System;


namespace Base.DomainModelLayer.Models
{
    public interface IAggregateRoot
    {
        Guid Id { get; }
    }
}
