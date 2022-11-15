using BaseDDD.DomainModelLayer.Interfaces;
using BaseDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Interfaces
{
    public interface IReportCreatorUnitOfWork : IUnitOfWork, IDisposable
    {
        IAccountRepository AccountRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IUserRepository UserRepository { get; }

        void CheckEventsAndUpdate(EventBus eventBus);
    }

}
