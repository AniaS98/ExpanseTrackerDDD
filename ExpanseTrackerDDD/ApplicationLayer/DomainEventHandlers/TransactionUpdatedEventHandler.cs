using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Events.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Helpers;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DomainEventHandlers
{
    public class TransactionUpdatedEventHandler : IEventHandler<TransactionUpdatedEvent>
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
                // Pobranie zmiennej Budzet
                var budget = this._budgetRepository.GetActiveByAccountIdAndCategory(eventData.Transaction.AccountId, eventData.Transaction.TransactionCategory);

                //Aktualizacja budżetu jeżeli istniał
                if (budget != null && eventData.Transaction.Status == TransactionStatus.Settled)
                {
                    budget.UpdateCurrentValue(moneyDifference);
                    this._budgetRepository.Update(budget);
                }
            }

            //Weryfikacja i dokonanie zmian na koncie, jeżeli transakcja jest rozliczona
            if (eventData.Transaction.Status == TransactionStatus.Settled)
            {
                AccountHelper.UpdateAccountAfterUpdateBalance(eventData.Account, eventData.Transaction, moneyDifference);
                this._accountRepository.Update(eventData.Account);
            }

        }

    }
}
