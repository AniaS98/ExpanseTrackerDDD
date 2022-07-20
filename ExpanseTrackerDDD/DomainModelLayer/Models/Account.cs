using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Base.DomainModelLayer.Events;
using Base.DomainModelLayer.Models;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public enum AccountType
    {
        BankAccount,
        Cash,
        SavingsAccount,
        BankAccountWithOverdraft,
        CreditCard,
        Investement,
        Loan
    }


    public class Account : AggregateRoot
    {
        public string Name { get; protected set; }
        public string AccountNumber { get; protected set; }
        public AccountType Type { get; protected set; }
        public Money AccountBalance { get; protected set; }
        public Currency AccountCurrency { get; protected set; }
        public string Color { get; protected set; }
        public Guid UserId { get; protected set; }

        /*
        private List<Transaction> transactions = new List<Transaction>();
        public ReadOnlyCollection<Transaction> Transactions { get { return this.transactions.AsReadOnly(); } }
        */
        public Account(Guid id, IDomainEventPublisher domainEventPublisher, string name, string accountName, AccountType type, Currency currency, Guid userId) : base(id, domainEventPublisher)
        {
            this.Id = new Guid();
            this.Name = name;
            this.AccountNumber = accountName;
            this.Type = type;
            this.AccountBalance = new Money(0,currency);
            this.AccountCurrency = currency;
            this.Color = "";
            this.UserId = userId;
            //this.transactions = new List<Transaction>();
        }

        public Account(Guid id, IDomainEventPublisher domainEventPublisher, string name, string accountName, AccountType type, Currency currency, Guid userId, string color) : base(id, domainEventPublisher)
        {
            this.Id = new Guid();
            this.Name = name;
            this.AccountNumber = accountName;
            this.Type = type;
            this.AccountBalance = new Money(0, currency);
            this.AccountCurrency = currency;
            this.Color = color;
            this.UserId = userId;
            //this.transactions = new List<Transaction>();
        }

        public void SetAccountBalance(decimal balance)
        {
            this.AccountBalance.Update(balance);
        }

        public void UpdateName(string name)
        {
            this.Name = name;
        }

        public void UpdateAccountNumber(string accountNumber)
        {
            this.AccountNumber = accountNumber;
        }

        public void UpdateColor(string color)
        {
            this.Color = color;
        }
        /*
        public void AddTransaction(Transaction transaction)
        {
            this.transactions.Add(transaction);
        }
        */


    }
}
