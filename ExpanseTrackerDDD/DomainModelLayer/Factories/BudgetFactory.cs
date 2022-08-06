using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Factories
{
    public class BudgetFactory
    {
        private IDomainEventPublisher _domainEventPublisher;

        public BudgetFactory(IDomainEventPublisher domainEventPublisher)
        {
            this._domainEventPublisher = domainEventPublisher;
        }

        public Budget Create(BudgetDto budget)
        {
            Currency currency = new Currency((CurrencyName)budget.Currency.Base, (CurrencyName)budget.Currency.Name);
            Money limit = new Money(budget.Limit.Amount, currency);

            return new Budget(new Guid(), this._domainEventPublisher, budget.Name, limit, (BudgetType)budget.Type, currency, budget.UserId);
        }

        public Budget RenewBudget(BudgetDto oldBudget)//dopisać żeby raport się restartował dzień po ostatnim dniu budżetu
        {
            Currency currency = new Currency((CurrencyName)oldBudget.Currency.Base, (CurrencyName)oldBudget.Currency.Name);
            Money limit = new Money(oldBudget.Limit.Amount, currency);
            return new Budget(new Guid(), this._domainEventPublisher, oldBudget.Name, limit, (BudgetType)oldBudget.Type, currency, oldBudget.UserId);
        }

    }
}
