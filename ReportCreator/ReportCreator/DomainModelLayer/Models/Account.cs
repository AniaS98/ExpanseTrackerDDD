using BaseDDD.DomainModelLayer.Models;
using System;
using ET_DML_M = ExpanseTrackerDDD.DomainModelLayer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models
{
    public class Account : AggregateRoot
    {
        public string Name { get; protected set; }
        public Money AccountBalance { get; protected set; }
        public Money Overdraft { get; protected set; }
        public CurrencyName Currency { get; protected set; }
        public Guid OwnerId { get; protected set; }


        public Account(Guid id, string name, decimal balance, CurrencyName currencyName, Guid ownerId, decimal overdraft=0) : base(id)
        {
            this.Name = name;
            this.AccountBalance = new Money(balance, currencyName);
            this.Overdraft = new Money(overdraft, currencyName);
            this.Currency = currencyName;
            this.OwnerId = ownerId;
        }

        public Account(ET_DML_M.Account account)
        {
            this.Id = account.Id;
            this.Name = account.Name;
            this.AccountBalance = account.Balance;
            this.Overdraft = account.Overdraft;
            this.Currency = account.CurrencyName;
            this.OwnerId = account.UserId;
        }

        public void UpdateAccountBalance(Money value)
        {
            AccountBalance += value;
        }

        public decimal CaluculateUtilization()
        {
            if (this.AccountBalance.Amount >= 0.00m)
                return 0.00m;
            
            return (-this.AccountBalance.Amount) / this.Overdraft.Amount;
        }




    }
}
