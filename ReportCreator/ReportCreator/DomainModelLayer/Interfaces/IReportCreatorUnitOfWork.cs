using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Interfaces
{
    public interface IReportCreatorUnitOfWork : IDisposable
    {
        IAccountRepository AccountRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        void Commit();
        void RejectChanges();
    }

}
