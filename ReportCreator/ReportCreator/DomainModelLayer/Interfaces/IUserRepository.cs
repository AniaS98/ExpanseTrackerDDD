using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        User GetUserById(Guid id);


    }
}
