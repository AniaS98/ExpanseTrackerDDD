using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        List<Account> GetAllAccountsByOwnerId(Guid id);
    }
}
