using System;
using System.Collections.Generic;
using System.Text;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using System.Security;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public class User : IAggregateRoot
    {
        public Guid Id { get; protected set; }
        public string Login { get; protected set; }
        public SecureString Password { get; protected set; }
        public string Firstname { get; protected set; }
        public string LastName { get; protected set; }
        //private List<purchase> purchases = new List<purchase>();
        //public ReadOnlyCollection<purchase> Purchases { get { return this.purchases.AsReadOnly(); } }



    }
}
