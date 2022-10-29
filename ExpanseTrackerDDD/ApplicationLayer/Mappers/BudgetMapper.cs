using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Mappers
{
    public class BudgetMapper
    {
        public List<BudgetDto> Map(IList<Budget> budgets)
        {
            List<BudgetDto> result = new List<BudgetDto>();
            foreach (Budget budget in budgets)
            {
                BudgetDto budgetDto = Map(budget);
                result.Add(budgetDto);
            }

            return result;
        }

        public BudgetDto Map(Budget budget)
        {
            BudgetDto result = new BudgetDto()
            {
                CurrentValue = Mappers.Map(budget.CurrentValue),
                EndTime = budget.EndTime,
                Id = budget.Id,
                Limit = Mappers.Map(budget.Limit),
                LimitUtilization = budget.LimitUtilization,
                Name = budget.Name,
                StartTime = budget.StartTime,
                Type = (BudgetTypeDto)budget.Type,
                BudgetCategoryDto = Mappers.Map(budget.BudgetCategory),
                AccountId = budget.AccountId,
                CurrentStatus = (BudgetStatusDto)budget.CurrentStatus
            };

            return result;
        }



    }
}
