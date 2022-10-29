using ExpanseTrackerDDD.DomainModelLayer.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models.Basic
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public ICollection<IDomainEvent> DomainEvents
        {
            get; protected set;
        }

        public Entity()
        { }

        public Entity(Guid id)
        {
            this.Id = id;
            this.DomainEvents = new List<IDomainEvent>();
        }        

        #region DomainEvent
        public void AddDomainEvent(IDomainEvent eventItem)
        {
            DomainEvents = DomainEvents ?? new List<IDomainEvent>();
            DomainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(IDomainEvent eventItem)
        {
            DomainEvents?.Remove(eventItem);
        }

        public void RemoveAllDomainEvents()
        {
            DomainEvents?.Clear();
        }

        #endregion
    }
}
