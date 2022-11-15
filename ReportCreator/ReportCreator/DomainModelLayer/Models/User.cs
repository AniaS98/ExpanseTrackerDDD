using BaseDDD.DomainModelLayer.Models;
using ET_DML_M = ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models
{

    public class User : AggregateRoot
    {
        public string Status { get; protected set; }

        public User(Guid id, string status) : base(id)
        {
            this.Status = status;
        }

        public User(ET_DML_M.User user)
        {
            this.Id = user.Id;
            this.Status = user.status.ToString();
        }

        public void ChangeUserStatus(string status)
        {
            this.Status = status;
        }

    }
}
