using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;

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


    public class Account : IAggregateRoot
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public string AccountNumber { get; protected set; }
        public AccountType Type { get; protected set; }
        public Money AccountBalance { get; protected set; }
        public Currency AccountCurrency { get; protected set; }
        public string Color { get; protected set; }

        private List<Transaction> transactions = new List<Transaction>();
        public ReadOnlyCollection<Transaction> Transactions { get { return this.transactions.AsReadOnly(); } }

        public Account(string name, string accountName, AccountType type, Currency currency)
        {
            this.Id = new Guid();
            this.Name = name;
            this.AccountNumber = accountName;
            this.Type = type;
            this.AccountBalance = new Money(0,currency);
            this.AccountCurrency = currency;
            this.Color = "";
            this.transactions = new List<Transaction>();
        }

        public Account(string name, string accountName, AccountType type, Currency currency, string color)
        {
            this.Id = new Guid();
            this.Name = name;
            this.AccountNumber = accountName;
            this.Type = type;
            this.AccountBalance = new Money(0, currency);
            this.AccountCurrency = currency;
            this.Color = color;
            this.transactions = new List<Transaction>();
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



    }
}
