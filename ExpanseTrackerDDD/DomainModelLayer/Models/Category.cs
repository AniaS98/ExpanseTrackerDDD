using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Models
{
    public class Category : ValueObject
    {
        public string Name { get; protected set; }
        public string IconPath { get; protected set; }
        public Guid TransactionId { get; protected set; }
        //public Transaction Transaction { get; protected set; }

        public Category(string name, string iconPath, Guid transactionId)
        {
            this.Name = name;
            this.IconPath = iconPath;
            this.TransactionId = transactionId;
            //this.Transaction = transaction;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
