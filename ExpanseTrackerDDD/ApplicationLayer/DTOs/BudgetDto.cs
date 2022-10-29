using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DTOs
{
    public enum BudgetTypeDto
    {
        Monthly,
        OneTime
    }
    public enum BudgetStatusDto
    {
        Active,
        Past
    }

    public class BudgetDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public MoneyDto Limit { get; set; }
        public MoneyDto CurrentValue { get; set; }
        public decimal LimitUtilization { get; set; }
        public BudgetTypeDto Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public BudgetStatusDto CurrentStatus { get; set; }
        public Guid AccountId { get; set; }
        public CategoryDto BudgetCategoryDto { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Budget: " + Id + "\n");
            sb.Append("Name: " + Name + "\n");
            sb.Append("Type: " + Type + "\n");
            sb.Append("Status: " + CurrentStatus + "\n");
            sb.Append("Limit details: " + CurrentValue + "/" + Limit + " (" + LimitUtilization + "%)\n");
            sb.Append("Limit applies between: " + StartTime.ToString("dd.MM.yyyy") + " - " + EndTime.ToString("dd.MM.yyyy") + "\n");

            return sb.ToString();
        }
    }
}
