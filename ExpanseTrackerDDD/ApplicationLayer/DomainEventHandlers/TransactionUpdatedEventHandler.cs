using BaseDDD.DomainModelLayer.Events;
using BaseDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Helpers;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DomainEventHandlers
{
    public class TransactionUpdatedEventHandler : IDomainEventHandler<TransactionUpdatedEvent>
    {
        private IBudgetRepository _budgetRepository;
        private IAccountRepository _accountRepository;

        public TransactionUpdatedEventHandler(IBudgetRepository budgetRepository, IAccountRepository accountRepository)
        {
            _budgetRepository = budgetRepository;
            _accountRepository = accountRepository;
        }

        public void Handle(TransactionUpdatedEvent eventData)
        {
            Money moneyDifference = eventData.Transaction.Value - eventData.Value;
            // Aktualizacja budżetu
            // Aktualizacja budżetu nastepuje tylko w przypadku gdy transakcja jest typu Expanse
            if (eventData.Transaction.Type == TransactionType.Expanse)
            {
                //Aktualizacja budżetu jeżeli istniał
                if (eventData.Budget != null && eventData.Transaction.Status == TransactionStatus.Settled)
                {
                    eventData.Budget.UpdateCurrentValue(moneyDifference);
                    this._budgetRepository.Update(eventData.Budget);
                }
            }

            //Weryfikacja i dokonanie zmian na koncie, jeżeli transakcja jest rozliczona
            if (eventData.Transaction.Status == TransactionStatus.Settled)
            {
                eventData.Account.UpdateBalance(moneyDifference);
                this._accountRepository.Update(eventData.Account);
            }

        }

    }
}
