using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.BudgetCommands
{
    public class CreateBudgetCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal LimitAmount { get; set; }
        public CurrencyName LimitCurrency { get; set; }
        public BudgetType Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public CategoryName CategoryName { get;  set; }
        public SubcategoryName SubcategoryName { get;  set; }
        public Guid AccountId { get; set; }
    }
}
