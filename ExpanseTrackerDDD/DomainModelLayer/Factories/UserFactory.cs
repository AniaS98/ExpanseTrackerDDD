using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Factories
{
    public class UserFactory
    {
        private IDomainEventPublisher _domainEventPublisher;

        public UserFactory(IDomainEventPublisher domainEventPublisher)
        {
            this._domainEventPublisher = domainEventPublisher;
        }
        public User Create(UserDto user)
        {
            return new User(new Guid(), _domainEventPublisher, user.Login, user.Password, user.FirstName, user.LastName);
        }


    }
}
