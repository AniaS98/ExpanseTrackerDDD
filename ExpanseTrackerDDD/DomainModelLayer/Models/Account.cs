using System;
using System.Collections.Generic;
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
        public float AccountBalance { get; protected set; }
        //public Currency AccountCurrency { get; protected set; }
        public string Color { get; protected set; }
        //public transkacje


    }
}
