using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using Microsoft.EntityFrameworkCore;

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
        public Money Balance { get; protected set; }
        public Money Overdraft { get; protected set; }
        public string Name { get; protected set; }
        public string AccountNumber { get; protected set; }
        public AccountType Type { get; protected set; }
        public CurrencyName CurrencyName { get; protected set; }
        public Guid UserId { get; protected set; } 

        protected Account(){}

        public Account(Guid id, string name, string accountNumber, AccountType type, CurrencyName currencyName, Guid userId, decimal balance = 0.0m, Money overdraft=null) : base(id)
        {
            this.Id = id;
            this.Balance = new Money(balance, currencyName);
            this.Overdraft = overdraft;
            this.Name = name;
            this.AccountNumber = accountNumber;
            this.Type = type;
            this.CurrencyName = currencyName;
            this.UserId = userId;
        }

        public void UpdateName(string name)
        {
            this.Name = name;
        }

        public void UpdateAccountNumber(string accountNumber)
        {
            this.AccountNumber = accountNumber;
        }

        public void UpdateType(AccountType type)
        {
            this.Type = type;
        }

        public void UpdateOriginalBalance(Money value)
        {
            this.Balance = value;
        }

        public void UpdateBalance(Money value)
        {
            this.Balance = value;
        }
    }
}
