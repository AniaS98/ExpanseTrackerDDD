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
    public class AccountRepository : Repository<Account>, IAccountRepository
    {
        public AccountRepository(ETContext context) : base(context) { }

        public Account GetAccountByName(string name)
        {
            return Context.Accounts.Where(a => a.Name == name).FirstOrDefault();
        }

        public Account GetAccountByNumber(string number)
        {
            return Context.Accounts.Where(a => a.Name == number).FirstOrDefault();
        }
        public List<Account> GetAllByUserId(Guid userId)
        {
            return Context.Accounts.Where(a => a.UserId == userId).ToList();
        }

    }
}
