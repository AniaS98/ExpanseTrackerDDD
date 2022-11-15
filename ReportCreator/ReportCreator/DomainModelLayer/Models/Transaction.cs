using BaseDDD.DomainModelLayer.Models;
using ET_DML_M = ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models
{
    public class Transaction : AggregateRoot
    {
        public string Name { get; protected set; }
        public Money Value { get; protected set; }
        public string Type { get; protected set; }
        public string Frequency { get; protected set; }
        public string Category { get; protected set; }
        public DateTime Date { get; protected set; }
        public string Status { get; protected set; }
        public Guid BudgetId { get; protected set; }
        public Guid AccountId { get; protected set; }

        public Transaction() { }
        public Transaction(Guid id, string name, Money value, string type, string frequency, string category, DateTime date, string status, Guid budgetId, Guid accountId) : base(id)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
            this.Frequency = frequency;
            this.Category = category;
            this.Date = date;
            this.Status = status;
            this.BudgetId = budgetId;
            this.AccountId = accountId;
        }

        public Transaction(ET_DML_M.Transaction transaction, Guid budgetId)
        {
            Id = transaction.Id;
            Name = transaction.Description;
            Value = transaction.Value;
            Type = transaction.Type.ToString();
            Frequency = transaction.Frequency.ToString();
            Category = transaction.Frequency.ToString();
            Date = transaction.TransactionDate;
            Status = transaction.Status.ToString();
            BudgetId = budgetId;
            AccountId = transaction.AccountId;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name + "\tof value: " + Value + "\tDate: " + Date);

            return sb.ToString();
        }




    }
}
