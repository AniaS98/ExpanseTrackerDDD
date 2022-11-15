using ReportCreator.DomainModelLayer.Interfaces;
using ReportCreator.DomainModelLayer.Models;
using ReportCreator.InfrastructureLayer.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.DomainModelLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(RCContext context) : base(context) { }

        public User GetUserById(Guid id)
        {
            return Context.Users.Where(u => u.Id == id).FirstOrDefault();
        }




    }
}
