using ReportCreator.DomainModelLayer.Interfaces;
using ReportCreator.DomainModelLayer.Models;
using ReportCreator.InfrastructureLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.DomainModelLayer.Repositories
{
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(RCContext context) : base(context) { }

        public List<Account> GetAllAccountsByOwnerId(Guid id)
        {
            return Context.Accounts.Where(x => x.OwnerId == id).ToList();
        }
    }
}
