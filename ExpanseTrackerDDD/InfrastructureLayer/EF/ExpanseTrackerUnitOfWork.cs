using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Data.Entity;
using ExpanseTrackerDDD.DomainModelLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using ExpanseTrackerDDD.DomainModelLayer.Models.Basic;
using ExpanseTrackerDDD.DomainModelLayer.Events.Interfaces;

namespace ExpanseTrackerDDD.InfrastructureLayer.EF
{
    public class ExpanseTrackerUnitOfWork : IExpanseTrackerUnitOfWork
    {
        private ETContext Context;
        private IDomainEventDispatcher eventDispatcher;

        public IUserRepository UserRepository { get; }

        public IAccountRepository AccountRepository { get; }

        public IBudgetRepository BudgetRepository { get; }

        public ITransactionRepository TransactionRepository { get; }

        public ExpanseTrackerUnitOfWork(ETContext context)
        {
            this.Context = context;
            this.UserRepository = new UserRepository(context);
            this.AccountRepository = new AccountRepository(context);
            this.BudgetRepository = new BudgetRepository(context);
            this.TransactionRepository = new TransactionRepository(context);
        }

        public void Commit()
        {
            //Pobieranie wszytkich zmian
            var domainEventEntities = Context.ChangeTracker.Entries<Entity>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Any()).ToArray();

            //Iteraja po wszystkich entity, w których zaszły zmiany
            foreach (var e in domainEventEntities)
            {
                //Pobieranie wszystkich zdarzeń danego entity
                var events = e.DomainEvents.ToArray();
                //Czyszczenie listy zdarzeń
                e.DomainEvents.Clear();
                //Obsługa zdarzeń
                foreach (var de in events)
                {
                    eventDispatcher.Dispatch(de);
                }
            }


            Context.SaveChanges();
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

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
