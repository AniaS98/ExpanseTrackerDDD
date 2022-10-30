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
    public enum UserStatus
    {
        LoggedOut,
        LoggedIn
    }

    public class User : AggregateRoot
    {
        public string Login { get; protected set; }
        public string Password { get; protected set; } 
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public UserStatus status { get; protected set; }

        public User() { }

        public User(Guid id, string login, string password, string firstName, string lastName) : base(id)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.status = UserStatus.LoggedIn;
        }

        public void UpdatePassword(string newPassword)
        {
            this.Password = newPassword;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("User: " + Id + "\n");
            sb.Append("Login: " + Login + "\n");
            sb.Append("Name: " + FirstName + " " + LastName + "\n");

            return sb.ToString();
        }


    }
}
