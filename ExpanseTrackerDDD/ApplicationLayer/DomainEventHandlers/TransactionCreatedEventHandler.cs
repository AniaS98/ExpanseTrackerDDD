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
    public class TransactionCreatedEventHandler: IEventHandler<TransactionCreatedEvent>
    {
        private IBudgetRepository _budgetRepository;
        private IAccountRepository _accountRepository;

        public TransactionCreatedEventHandler(IBudgetRepository budgetRepository, IAccountRepository accountRepository)
        {
            _budgetRepository = budgetRepository;
            _accountRepository = accountRepository;
        }

        public void Handle(TransactionCreatedEvent eventData)
        {
            // Aktualizacja budżetu
            // Aktualizacja budżetu nastepuje tylko w przypadku gdy transakcja jest typu Expanse
            if(eventData.Transaction.Type == TransactionType.Expanse)
            {
                // Pobranie zmiennej Budzet
                var budget = this._budgetRepository.GetActiveByAccountIdAndCategory(eventData.Transaction.AccountId, eventData.Transaction.TransactionCategory);
                
                // Aktualizacja budżetu jeżeli istniał a transakcja jest rozliczona
                if (budget != null && eventData.Transaction.Status == TransactionStatus.Settled)
                {
                    budget.UpdateCurrentValue(eventData.Transaction.Value);
                    this._budgetRepository.Update(budget);
                }
            }

            // Aktualizacja konta
            //Weryfikacja i dokonanie zmian na koncie jeżeli transakcja jest została zakończona
            if(eventData.Transaction.Status == TransactionStatus.Settled)
            {
                AccountHelper.UpdateAccountBalance(eventData.Account, eventData.Transaction);
                this._accountRepository.Update(eventData.Account);
            }
            
        }

    }
}
