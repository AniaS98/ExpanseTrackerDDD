using BaseDDD.DomainModelLayer.Events;
using BaseDDD.DomainModelLayer.Models;
using RC_DML_E_IE = ReportCreator.DomainModelLayer.Events.IntegrationEvents;
using ReportCreator.DomainModelLayer.Interfaces;
using ET_DML_E = ExpanseTrackerDDD.DomainModelLayer.Events;
using RC_DML_M = ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.IntegrationEventHandlers
{
    public class TransactionUpdatedEventHandler : IEventHandler<RC_DML_E_IE.TransactionUpdatedEvent, ET_DML_E.TransactionUpdatedEvent>
    {
        private IAccountRepository _accountRepository;

        public TransactionUpdatedEventHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Handle(RC_DML_E_IE.TransactionUpdatedEvent eventData)
        {
            Money moneyDifference = eventData.Transaction.Value - eventData.Value;

            //Weryfikacja i dokonanie zmian na koncie, jeżeli transakcja jest rozliczona
            if (eventData.Transaction.Status == "Settled")
            {
                eventData.Account.UpdateAccountBalance(moneyDifference);
                this._accountRepository.Update(eventData.Account);
            }
        }

        public RC_DML_E_IE.TransactionUpdatedEvent Map(ET_DML_E.TransactionUpdatedEvent eventData)
        {
            return new RC_DML_E_IE.TransactionUpdatedEvent(new RC_DML_M.Transaction(eventData.Transaction, eventData.Budget.Id),
                new RC_DML_M.Account(eventData.Account),
                eventData.Value);
        }
    }
}
