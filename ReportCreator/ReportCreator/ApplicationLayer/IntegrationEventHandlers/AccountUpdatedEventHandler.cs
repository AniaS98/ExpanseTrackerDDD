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
    public class AccountUpdatedEventHandler : IEventHandler<RC_DML_E_IE.AccountUpdatedEvent, ET_DML_E_IE.AccountUpdatedEvent>
    {
        private IAccountRepository _accountRepository;

        public AccountUpdatedEventHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Handle(RC_DML_E_IE.AccountUpdatedEvent eventData)
        {
            // Aktualizacja konta
            this._accountRepository.Update(eventData.Account);
        }


        public RC_DML_E_IE.AccountUpdatedEvent Map(ET_DML_E_IE.AccountUpdatedEvent eventData)
        {
            RC_DML_M.Account account = new RC_DML_M.Account(eventData.Account);
            return new RC_DML_E_IE.AccountUpdatedEvent(account);
        }

    }
}
