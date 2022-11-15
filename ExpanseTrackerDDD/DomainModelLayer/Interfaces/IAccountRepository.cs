using BaseDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpanseTrackerDDD.DomainModelLayer.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetAccountByName(string name);
        Account GetAccountByNumber(string number);
        List<Account> GetAllByUserId(Guid userId);
    }
}
