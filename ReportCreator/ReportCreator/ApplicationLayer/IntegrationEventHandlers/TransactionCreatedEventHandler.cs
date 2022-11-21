using BaseDDD.DomainModelLayer.Events;
using ReportCreator.DomainModelLayer.Events;
using RC_DML_E_IE = ReportCreator.DomainModelLayer.Events.IntegrationEvents;
using ET_DML_E = ExpanseTrackerDDD.DomainModelLayer.Events;
using ReportCreator.DomainModelLayer.Interfaces;
using RC_DML_M = ReportCreator.DomainModelLayer.Models;
using ReportCreator.InfrastructureLayer.EF;
using System;
using System.Collections.Generic;
using System.Text;
using BaseDDD.DomainModelLayer.Models;

namespace ReportCreator.ApplicationLayer.IntegrationEventHandlers
{
    public class TransactionCreatedEventHandler : IEventHandler<RC_DML_E_IE.TransactionCreatedEvent, ET_DML_E.TransactionCreatedEvent>
    {
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;

        public TransactionCreatedEventHandler(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public void Handle(RC_DML_E_IE.TransactionCreatedEvent eventData)
        {
            // Dodanie transakcji 
            this._transactionRepository.Insert(eventData.Transaction);

            // Aktualizacja konta
            this._accountRepository.Update(eventData.Account);
        }

        public RC_DML_E_IE.TransactionCreatedEvent Map(ET_DML_E.TransactionCreatedEvent eventData)
        {
            return new RC_DML_E_IE.TransactionCreatedEvent(new RC_DML_M.Transaction(eventData.Transaction.Id, 
                eventData.Transaction.Description, eventData.Transaction.Value, eventData.Transaction.Type.ToString(), 
                eventData.Transaction.Frequency.ToString(), eventData.Transaction.TransactionCategory.ToString(), 
                eventData.Transaction.TransactionDate, eventData.Transaction.Status.ToString(), eventData.Budget.Id,
                eventData.Transaction.AccountId), new RC_DML_M.Account(eventData.Account.Id, eventData.Account.Name, 
                eventData.Account.Balance.Amount, eventData.Account.CurrencyName, eventData.Account.UserId, 
                eventData.Account.Overdraft.Amount));
        }
    }
}
