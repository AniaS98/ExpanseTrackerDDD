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
    public class AccountCreatedEventHandler : IEventHandler<RC_DML_E_IE.AccountCreatedEvent, ET_DML_E_IE.AccountCreatedEvent>
    {
        private IAccountRepository _accountRepository;

        public AccountCreatedEventHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Handle(RC_DML_E_IE.AccountCreatedEvent eventData)
        {
            // Dodanie konta
            this._accountRepository.Insert(eventData.Account);
        }


        public RC_DML_E_IE.AccountCreatedEvent Map(ET_DML_E_IE.AccountCreatedEvent eventData)
        {
            RC_DML_M.Account account = new RC_DML_M.Account(eventData.Account);
            return new RC_DML_E_IE.AccountCreatedEvent(account);
        }
    }
}
