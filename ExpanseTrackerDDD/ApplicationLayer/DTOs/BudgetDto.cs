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
        public string Name { get; set; }
        public Guid Id { get; set; }
        public MoneyDto Limit { get; set; }
        public MoneyDto CurrentValue { get; set; }
        public MoneyDto LimitUtilization { get; set; }
        public CurrencyDto Currency { get; set; }
        public BudgetTypeDto Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid UserId { get; set; }
    }
}
