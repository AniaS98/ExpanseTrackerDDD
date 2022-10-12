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
    public class User : Entity
    {
        public string Login { get; protected set; }
        public SecureString Password { get; protected set; } 
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }


        public User(Guid id, string login, SecureString password, string firstName, string lastName) : base(id)
        {
            this.Id = id;
            this.Login = login;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public void UpdatePassword(SecureString newPassword)
        {
            this.Password = newPassword;
        }


    }
}
