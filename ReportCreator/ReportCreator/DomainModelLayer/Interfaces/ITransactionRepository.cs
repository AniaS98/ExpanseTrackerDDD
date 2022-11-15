using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Interfaces
{
    public interface ITransactionRepository : IRepository<Transaction>
    {
        List<Transaction> GetAllTransactionsByAccountId(Guid accountId);
    }
}
