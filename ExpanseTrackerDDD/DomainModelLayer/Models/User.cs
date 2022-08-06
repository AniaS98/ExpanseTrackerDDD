using System;
using System.Collections.Generic;
using System.Text;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using System.Security;
using System.Collections.ObjectModel;
using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using ExpanseTrackerDDD.DomainModelLayer.Events;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public class User : AggregateRoot
    {
        public Guid Id { get; protected set; }
        public string Login { get; protected set; }
        public string Password { get; protected set; } //tymczasowe rozwiązanie
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public ICollection<Account> Accounts { get; protected set; }
        public ICollection<Budget> Budgets { get; protected set; }

        /// <summary>
        /// TYMCZASOWE TYMCZASOWE TYMCZASOWE - DO MARKOWANIA W LOGOWANIU W GUI
        /// </summary>
        public User(Guid id, IDomainEventPublisher domainEventPublisher) : base(id, domainEventPublisher)
        {
            this.Id = id;
        }
        public User(Guid id, IDomainEventPublisher domainEventPublisher, string login, string password, string firstName, string lastName) : base(id, domainEventPublisher)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Accounts = new List<Account>();
            this.Budgets = new List<Budget>();
        }

        public void UpdatePassword(string newPassword)
        {
            this.Password = newPassword;
        }


    }
}
