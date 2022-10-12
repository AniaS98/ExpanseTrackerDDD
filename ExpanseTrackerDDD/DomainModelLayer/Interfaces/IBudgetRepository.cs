using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpanseTrackerDDD.DomainModelLayer.Interfaces
{
    public interface IBudgetRepository : IRepository<Budget>
    {
        List<Budget> GetAllByAccountId(Guid accountId);
        Budget GetActiveByAccountId(Guid accountId);
    }
}
