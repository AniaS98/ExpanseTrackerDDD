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
using ExpanseTrackerDDD.DomainModelLayer.Events.Implementations;

namespace ExpanseTrackerDDD.InfrastructureLayer.EF
{
    public class ExpanseTrackerUnitOfWork : IExpanseTrackerUnitOfWork
    {
        private ETContext Context;
        private IDomainEventDispatcher EventDispatcher;
        private IEventBus EventBus;

        public IUserRepository UserRepository { get; }

        public IAccountRepository AccountRepository { get; }

        public IBudgetRepository BudgetRepository { get; }

        public ITransactionRepository TransactionRepository { get; }

        public ExpanseTrackerUnitOfWork(ETContext context, IDomainEventDispatcher eventDispatcher, IEventBus eventBus)
        {
            this.Context = context;
            this.UserRepository = new UserRepository(context);
            this.AccountRepository = new AccountRepository(context);
            this.BudgetRepository = new BudgetRepository(context);
            this.TransactionRepository = new TransactionRepository(context);
            this.EventDispatcher = eventDispatcher;
            this.EventBus = eventBus;
        }

        public void Commit()
        {
            //Pobieranie wszytkich zmian
            var domainEventEntities = Context.ChangeTracker.Entries<Entity>()
                .Select(x => x.Entity)
                .Where(x => x.DomainEvents.Any()).ToArray();

            //Iteraja po wszystkich entity, w których zaszły zmiany
            foreach (var dee in domainEventEntities)
            {
                //Pobieranie wszystkich zdarzeń danego entity
                var events = dee.DomainEvents.ToArray();
                //Czyszczenie listy zdarzeń
                dee.DomainEvents.Clear();
                //Obsługa zdarzeń
                foreach (Event e in events)
                {
                    EventDispatcher.Dispatch(e);
                    if(e is IIntegrationEvent)
                        EventBus.Publish(e);
        
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
