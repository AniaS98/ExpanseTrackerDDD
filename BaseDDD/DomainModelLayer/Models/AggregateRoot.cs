using System;
using System.Collections.Generic;
using System.Text;

namespace BaseDDD.DomainModelLayer.Models
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        //protected IDomainEventPublisher DomainEventPublisher { get; set; }

        public AggregateRoot(Guid id) : base(id)
        {

        }

        public AggregateRoot()
        { }

    }
}
