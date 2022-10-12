using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DomainEventHandlers
{
    public class TransactionCreatedEventDomainEventHandler : IEventHandler<TransactionCreatedEvent>
    {
        private IBudgetRepository _budgetRepository;

        public TransactionCreatedEventDomainEventHandler(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }

        public void Handle(TransactionCreatedEvent eventData)
        {
            // get budget
            var budget = _budgetRepository.GetActiveByAccountId(eventData.AccountId);

            // update the budget
            budget.UpdateCurrentValue(eventData.Value);
        }

    }
}
