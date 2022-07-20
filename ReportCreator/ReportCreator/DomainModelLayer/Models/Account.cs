using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models
{
    public class Account
    {
        public Guid AccountId { get; protected set; }
        public string Name { get; protected set; }
        public Money AccountBalance { get; protected set; }
        public string Currency { get; protected set; }
        /*
        private List<Transaction> transactions = new List<Transaction>();
        public ReadOnlyCollection<Transaction> Transactions { get { return this.transactions.AsReadOnly(); } }
        */
        public Account(Guid id, string name, Money balance, string currency)
        {
            this.AccountId = id;
            this.Name = name;
            this.AccountBalance = balance;
            this.Currency = currency;
            //this.transactions = new List<Transaction>();
        }




    }
}
