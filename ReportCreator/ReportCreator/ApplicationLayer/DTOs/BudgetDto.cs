using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.DTOs
{
    public class BudgetDto
    {
        public Guid Id { get; set; }
        public MoneyDto limit { get; set; }
        public Guid AccountId { get; set; }
    }
}
