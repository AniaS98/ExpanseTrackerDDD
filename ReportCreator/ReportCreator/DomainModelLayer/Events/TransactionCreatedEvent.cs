using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Events
{
    public class TransactionCreatedEvent //: IDomainEvent
    {
        public string Name { get;  set; }
        public decimal MoneyAmount { get; protected set; }
        public string MoneyCurrencyName { get; protected set; }
        public string Type { get;  set; }
        public string Frequency { get;  set; }
        public string Category { get;  set; }
        public DateTime Date { get;  set; }
        public string Status { get;  set; }
        public Guid BudgetId { get;  set; }
        public Guid AccountId { get;  set; }

        public TransactionCreatedEvent(string name, decimal moneyAmount, string moneyCurrencyName, string type, string frequency, string category, DateTime date, string status, Guid budgetId, Guid accountId)
        {
            this.Name = name;
            this.MoneyAmount = moneyAmount;
            this.MoneyCurrencyName = moneyCurrencyName;
            this.Type = type;
            this.Frequency = frequency;
            this.Category = category;
            this.Date = date;
            this.Status = status;
            this.BudgetId = budgetId;
            this.AccountId = accountId;
        }


    }
}
