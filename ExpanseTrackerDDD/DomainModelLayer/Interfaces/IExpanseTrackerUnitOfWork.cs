using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Interfaces
{
    public interface IExpanseTrackerUnitOfWork : IDisposable, IUnitOfWork
    {
        IRepository<User> UserRepository { get; }
        IRepository<Account> AccountRepository { get; }
        IRepository<Budget> BudgetRepository { get; }
        IRepository<Transaction> TransactionRepository { get; }

    }
}
