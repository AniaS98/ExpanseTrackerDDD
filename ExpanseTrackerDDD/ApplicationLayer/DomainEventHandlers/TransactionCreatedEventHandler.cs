using BaseDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Helpers;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DomainEventHandlers
{
    public class TransactionCreatedEventHandler: IDomainEventHandler<TransactionCreatedEvent>
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
                // Aktualizacja budżetu jeżeli istniał a transakcja jest rozliczona
                if (eventData.Budget != null && eventData.Transaction.Status == TransactionStatus.Settled)
                {
                    eventData.Budget.UpdateCurrentValue(eventData.Transaction.Value);
                    this._budgetRepository.Update(eventData.Budget);
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
