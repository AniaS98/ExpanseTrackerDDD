using BaseDDD.DomainModelLayer.Events;
using BaseDDD.DomainModelLayer.Events.Implementations;
using BaseDDD.DomainModelLayer.Models;
using Microsoft.EntityFrameworkCore;
using ReportCreator.DomainModelLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportCreator.InfrastructureLayer.EF
{
    public class ReportCreatorUnitOfWork : IReportCreatorUnitOfWork
    {
        private RCContext Context;
        private IIntegrationEventDispatcher EventDispatcher;
        public IAccountRepository AccountRepository { get; }

        public ITransactionRepository TransactionRepository { get; }

        public IUserRepository UserRepository { get; }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public void RejectChanges()
        {
            foreach (var entry in Context.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
            }
        }

        public void CheckEventsAndUpdate(EventBus eventBus)
        {
            //Iteracja po zdarzeniach i ich obsługa
            foreach(IIntegrationEvent e in eventBus.IntegrationEvents)
            {
                EventDispatcher.Dispatch(e);
            }
            Context.SaveChanges();
        }

    }
}
