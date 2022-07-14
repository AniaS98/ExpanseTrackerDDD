using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models
{
    public class Transaction
    {
        public Money Value { get; protected set; }
        public string Type { get; protected set; }
        public string Categpry { get; protected set; }
        public DateTime Date { get; protected set; }
        public string Status { get; protected set; }
        public Guid BudgetId { get; protected set; }

        public Transaction(Money value, string type, string category, DateTime date, string status, Guid budgetId)
        {
            this.Value = value;
            this.Type = type;
            this.Categpry = category;
            this.Date = date;
            this.Status = status;
            this.BudgetId = budgetId;
        }






    }
}
