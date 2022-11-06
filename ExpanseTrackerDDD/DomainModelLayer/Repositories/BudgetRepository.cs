using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.InfrastructureLayer;
using ExpanseTrackerDDD.InfrastructureLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpanseTrackerDDD.DomainModelLayer.Repositories
{
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        public BudgetRepository(ETContext context) : base(context) { }

        public List<Budget> GetAllByAccountId(Guid accountId)
        {
            return Context.Budgets.Where(b => b.AccountId == accountId).ToList();
        }

        public Budget GetActiveByAccountId(Guid accountId)
        {
            return Context.Budgets.Where(b => b.AccountId == accountId && b.CurrentStatus == BudgetStatus.Active).FirstOrDefault();
        }

        public Budget GetActiveByAccountIdAndCategory(Guid accountId, Category category)
        {
            return Context.Budgets.Where(b => b.AccountId == accountId && b.CurrentStatus == BudgetStatus.Active && 
            b.BudgetCategory == category).FirstOrDefault();
        }
    }
}
