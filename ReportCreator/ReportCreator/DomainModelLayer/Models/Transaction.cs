using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models
{
    public class Transaction
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

        public Transaction(string name, Money value, string type, string frequency, string category, DateTime date, string status, Guid budgetId)
        {
            this.Name = name;
            this.Value = value;
            this.Type = type;
            this.Frequency = frequency;
            this.Category = category;
            this.Date = date;
            this.Status = status;
            this.BudgetId = budgetId;
            this.AccountId = AccountId;
        }






    }
}
