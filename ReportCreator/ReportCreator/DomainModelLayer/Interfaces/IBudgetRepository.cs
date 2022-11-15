using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Interfaces
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        Budget GetBudgetById(Guid id);
    }
}
