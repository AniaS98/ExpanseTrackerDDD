using ReportCreator.DomainModelLayer.Interfaces;
using ReportCreator.DomainModelLayer.Models;
using ReportCreator.InfrastructureLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.DomainModelLayer.Repositories
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(RCContext context) : base(context) { }

        public List<Transaction> GetAllTransactionsByAccountId(Guid id)
        {
            return Context.Transactions.Where(t => t.AccountId == id).ToList();
        }

    }
}
