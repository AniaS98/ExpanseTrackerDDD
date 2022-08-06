using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Factories
{
    public class AccountFactory
    {
        private IDomainEventPublisher _domainEventPublisher;

        public AccountFactory(IDomainEventPublisher domainEventPublisher)
        {
            this._domainEventPublisher = domainEventPublisher;
        }

        public Account Create(AccountDto account)
        {
            Currency currency = new Currency((CurrencyName)account.AccountCurrency.Base, (CurrencyName)account.AccountCurrency.Name);

            return new Account(new Guid(), _domainEventPublisher, account.Name, account.AccountNumber, (AccountType)account.Type, currency, new Money(0, currency), account.UserId);
        }


    }
}
