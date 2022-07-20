using System;
using System.Collections.Generic;
using System.Text;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using System.Security;
using System.Collections.ObjectModel;
using Base.DomainModelLayer.Models;
using Base.DomainModelLayer.Events;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public class User : AggregateRoot
    {
        public Guid Id { get; protected set; }
        public string Login { get; protected set; }
        public SecureString Password { get; protected set; }
        public string Firstname { get; protected set; }
        public string LastName { get; protected set; }
        /*
        private List<Account> accounts = new List<Account>();
        public ReadOnlyCollection<Account> Accounts { get { return this.accounts.AsReadOnly(); } }

        private List<Budget> budgets = new List<Budget>();
        public ReadOnlyCollection<Budget> Budgets { get { return this.budgets.AsReadOnly(); } }
        */
        public User(Guid id, IDomainEventPublisher domainEventPublisher, string login, SecureString password, string firstName, string lastName) : base(id, domainEventPublisher)
        {
            this.Id = new Guid();
            this.Login = login;
            this.Password = password;
            this.Firstname = firstName;
            this.LastName = lastName;
            //this.accounts = new List<Account>();
            //this.budgets = new List<Budget>();
        }


    }
}
