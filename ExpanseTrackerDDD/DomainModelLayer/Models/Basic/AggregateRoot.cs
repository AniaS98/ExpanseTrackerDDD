using ExpanseTrackerDDD.DomainModelLayer.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models.Basic
{
    public abstract class AggregateRoot : Entity, IAggregateRoot
    {
        protected IDomainEventPublisher DomainEventPublisher { get; set; }

        public AggregateRoot(Guid id, IDomainEventPublisher domainEventPublisher) : base(id)
        {
            if (domainEventPublisher == null)
                throw new ArgumentNullException("EventPublisher is not initialized");

            this.DomainEventPublisher = domainEventPublisher;
        }


    }
}
