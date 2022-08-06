using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;

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
        public Currency AccountCurrency { get; protected set; }
        public string Color { get; protected set; }
        public Guid UserId { get; protected set; }
        //public User User { get; protected set; }
        public ICollection<Transaction> Transactions { get; protected set; }

        public Account(Guid id, IDomainEventPublisher domainEventPublisher, string name, string accountNumber, AccountType type, Currency accountCurrency, Money accountBalance, Guid userId) : base(id, domainEventPublisher)
        {
            this.Id = id;
            this.Name = name;
            this.AccountNumber = accountNumber;
            this.Type = type;
            this.AccountCurrency = accountCurrency;
            this.Color = "";
            //this.User = user;
            this.UserId = userId;
            this.Transactions = new List<Transaction>();
        }

        public Account(Guid id, IDomainEventPublisher domainEventPublisher, string name, string accountNumber, AccountType type, Currency accountCurrency, Money accountBalance, Guid userId, string color) : base(id, domainEventPublisher)
        {
            this.Id = new Guid();
            this.Name = name;
            this.AccountNumber = accountNumber;
            this.Type = type;
            this.AccountCurrency = accountCurrency;
            this.Color = color;
            //this.User = user;
            this.UserId = userId;
            this.Transactions = new List<Transaction>();
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



    }
}
