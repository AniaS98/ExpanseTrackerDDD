using BaseDDD.DomainModelLayer.Events;
using ReportCreator.DomainModelLayer.Interfaces;
using RC_DML_E_IE = ReportCreator.DomainModelLayer.Events.IntegrationEvents;
using ET_DML_E_IE = ExpanseTrackerDDD.DomainModelLayer.Events.IntegrationEvents;
using RC_DML_M = ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.IntegrationEventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<RC_DML_E_IE.UserCreatedEvent, ET_DML_E_IE.UserCreatedEvent>
    {
        private IUserRepository _userRepository;

        public UserCreatedEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(RC_DML_E_IE.UserCreatedEvent eventData)
        {
            //Dodanie użytkownika do bazy
            this._userRepository.Insert(eventData.User);
        }

        public RC_DML_E_IE.UserCreatedEvent Map(ET_DML_E_IE.UserCreatedEvent eventData)
        {
            RC_DML_M.User user = new RC_DML_M.User(eventData.User);
            return new RC_DML_E_IE.UserCreatedEvent(user);
        }
    }
}
