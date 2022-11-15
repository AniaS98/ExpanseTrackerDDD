using BaseDDD.DomainModelLayer.Models;
using ET_DML_M = ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models
{
    public class Budget : AggregateRoot
    {
        public Money Limit { get; protected set; }
        public Guid AccountId { get; protected set; }

        public Budget(Guid id, Money limit, Guid accountId) : base(id)
        {
            this.Limit = limit;
            this.AccountId = accountId;
        }

        public Budget(ET_DML_M.Budget budget)
        {
            this.Id = budget.Id;
            this.Limit = budget.Limit;
            this.AccountId = budget.AccountId;
        }

    }
}
