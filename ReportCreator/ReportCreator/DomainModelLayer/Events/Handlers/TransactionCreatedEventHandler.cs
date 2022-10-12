using ReportCreator.DomainModelLayer.Interfaces;
using ReportCreator.DomainModelLayer.Models;
using ReportCreator.InfrastructureLayer.EF;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Events.Handlers
{
    public class TransactionCreatedEventHandler : IObserver
    {
        private IAccountRepository _accountRepository;

        public TransactionCreatedEventHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void Update<T>(IEventListener<T> listener) where T : IDomainEvent
        {
        }

        public void Handle(TransactionCreatedEvent eventData)
        {
            //Create Transaction
            Money money = new Money(eventData.MoneyAmount, eventData.MoneyCurrencyName);
            Transaction transaction = new Transaction(eventData.Name, money, eventData.Type, eventData.Frequency, eventData.Category, eventData.Date, eventData.Status, eventData.BudgetId, eventData.AccountId);

            //Add transaction to the account
            Account account = this._accountRepository.Get(eventData.AccountId);

            if (account == null)
                throw new Exception($"Account with Id '{eventData.AccountId}' does not exist!");

            account.UpdateAccountBalance(money);

        }




    }
}
