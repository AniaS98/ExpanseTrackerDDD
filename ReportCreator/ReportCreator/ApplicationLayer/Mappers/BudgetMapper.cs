using ReportCreator.ApplicationLayer.DTOs;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.Mappers
{
    public class BudgetMapper
    {
        public BudgetDto Map(Budget budget)
        {
            return new BudgetDto()
            {
                Id = budget.Id,
                limit = MoneyMapper.Map(budget.Limit),
                AccountId = budget.AccountId
            };

        }
    }
}
