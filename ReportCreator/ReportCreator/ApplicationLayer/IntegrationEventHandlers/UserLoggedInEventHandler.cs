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
    public class UserLoggedInEventHandler : IEventHandler<RC_DML_E_IE.UserLoggedInEvent, ET_DML_E_IE.UserLoggedInEvent>
    {
        private IUserRepository _userRepository;

        public UserLoggedInEventHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Handle(RC_DML_E_IE.UserLoggedInEvent eventData)
        {
            //Dokonanie zmiany na obiekcie
            eventData.User.ChangeUserStatus("LoggedIn");

            //Aktualizacja użytkownika
            this._userRepository.Update(eventData.User);
        }


        public RC_DML_E_IE.UserLoggedInEvent Map(ET_DML_E_IE.UserLoggedInEvent eventData)
        {
            RC_DML_M.User user = new RC_DML_M.User(eventData.User);
            return new RC_DML_E_IE.UserLoggedInEvent(user);
        }


    }
}
