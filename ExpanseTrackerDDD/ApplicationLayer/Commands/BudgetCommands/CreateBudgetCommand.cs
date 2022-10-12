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
        public List<CategoryName> CategoryNames { get;  set; }
        public List<SubcategoryName> SubcategoryNames { get;  set; }
        public Guid AccountId { get; set; }
    }
}
