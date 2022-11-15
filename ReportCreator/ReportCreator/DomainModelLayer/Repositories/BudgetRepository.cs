using ReportCreator.DomainModelLayer.Interfaces;
using ReportCreator.DomainModelLayer.Models;
using ReportCreator.InfrastructureLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.DomainModelLayer.Repositories
{
    public class BudgetRepository : Repository<Budget>, IBudgetRepository
    {
        public BudgetRepository(RCContext context) : base(context) { }

        public Budget GetBudgetById(Guid id)
        {
            return Context.Budgets.Where(u => u.Id == id).FirstOrDefault();
        }
    }
}
