using BaseDDD.DomainModelLayer.Interfaces;
using BaseDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Interfaces
{
    public interface IExpanseTrackerUnitOfWork : IUnitOfWork,  IDisposable
    {
        EventBus EventBus { get; }
        IUserRepository UserRepository { get; }
        IAccountRepository AccountRepository { get; }
        IBudgetRepository BudgetRepository { get; }
        ITransactionRepository TransactionRepository { get; }

    }
}
