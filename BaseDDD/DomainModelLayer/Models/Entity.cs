using BaseDDD.DomainModelLayer.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseDDD.DomainModelLayer.Models
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        public ICollection<IDomainEvent> DomainEvents
        {
            get; protected set;
        }

        public ICollection<IIntegrationEvent> IntegrationEvents
        {
            get; protected set;
        }

        public Entity()
        { }

        public Entity(Guid id)
        {
            this.Id = id;
            this.DomainEvents = new List<IDomainEvent>();
            this.IntegrationEvents = new List<IIntegrationEvent>();
        }        

        #region DomainEvent
        public void AddDomainEvent(IEvent eventItem)
        {
            DomainEvents = DomainEvents ?? new List<IDomainEvent>();
            DomainEvents.Add((IDomainEvent)eventItem);
        }

        public void RemoveDomainEvent(IEvent eventItem)
        {
            DomainEvents?.Remove((IDomainEvent)eventItem);
        }

        public void RemoveAllDomainEvents()
        {
            DomainEvents?.Clear();
        }

        #endregion

        #region IntegrationEvent
        public void AddIntegrationEvent(IEvent eventItem)
        {
            IntegrationEvents = IntegrationEvents ?? new List<IIntegrationEvent>();
            IntegrationEvents.Add((IIntegrationEvent)eventItem);
        }

        public void RemoveIntegrationEvent(IEvent eventItem)
        {
            IntegrationEvents?.Remove((IIntegrationEvent)eventItem);
        }

        public void RemoveAllIntegrationEvents()
        {
            IntegrationEvents?.Clear();
        }

        #endregion

        #region Event
        public void AddEvent(IEvent eventItem)
        {
            AddDomainEvent(eventItem);
            AddIntegrationEvent(eventItem);
        }

        public void RemoveEvent(IEvent eventItem)
        {
            RemoveDomainEvent(eventItem);
            RemoveIntegrationEvent(eventItem);
        }

        public void RemoveAllEvents()
        {
            RemoveAllDomainEvents();
            RemoveAllIntegrationEvents();
        }

        #endregion
    }
}
