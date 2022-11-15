using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpanseTrackerDDD.DomainModelLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using ExpanseTrackerDDD.DomainModelLayer.Events.Implementations;
using BaseDDD.DomainModelLayer.Events;
using BaseDDD.DomainModelLayer.Models;
using BaseDDD.DomainModelLayer.Events.Implementations;

namespace ExpanseTrackerDDD.InfrastructureLayer.EF
{
    public class ExpanseTrackerUnitOfWork : IExpanseTrackerUnitOfWork
    {
        private ETContext Context;
        private IDomainEventDispatcher EventDispatcher;

        public EventBus EventBus { get; }
        public IUserRepository UserRepository { get; }

        public IAccountRepository AccountRepository { get; }

        public IBudgetRepository BudgetRepository { get; }

        public ITransactionRepository TransactionRepository { get; }

        public ExpanseTrackerUnitOfWork(ETContext context, IDomainEventDispatcher eventDispatcher, EventBus eventBus)
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
                foreach (DomainEvent e in events)
                {
                    EventDispatcher.Dispatch(e);        
                }

            }


            //Pobieranie wszytkich zmian
            var integrationEventEntities = Context.ChangeTracker.Entries<Entity>()
                .Select(x => x.Entity)
                .Where(x => x.IntegrationEvents.Any()).ToArray();

            //Iteraja po wszystkich entity, w których zaszły zmiany
            foreach (var iee in integrationEventEntities)
            {
                //Pobieranie wszystkich zdarzeń danego entity
                var events = iee.IntegrationEvents.ToArray();
                //Czyszczenie listy zdarzeń
                iee.IntegrationEvents.Clear();
                //Przeniesienie do Busa
                foreach (IEvent e in events)
                {
                    EventBus.Add(e);
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
