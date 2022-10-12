using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Interfaces
{
    public interface IExpanseTrackerUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IAccountRepository AccountRepository { get; }
        IBudgetRepository BudgetRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        void Commit();
        void RejectChanges();

    }
}
