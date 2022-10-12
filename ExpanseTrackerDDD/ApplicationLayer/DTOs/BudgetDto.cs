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
        public Guid AccountId { get; set; }
        public List<CategoryDto> Categories { get; set; }
    }
}
