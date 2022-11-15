using System;
using System.Collections.Generic;
using System.IO;
using ExpanseTrackerDDD.ApplicationLayer.Commands.BudgetCommands;
using ExpanseTrackerDDD.ApplicationLayer.Commands.Handlers;
using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.ApplicationLayer.Mappers;
using ExpanseTrackerDDD.ApplicationLayer.Queries;
using ExpanseTrackerDDD.ApplicationLayer.Queries.Handlers;
using ExpanseTrackerDDD.DomainModelLayer.Factories;
using ET_DML_I = ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ET_DML_R = ExpanseTrackerDDD.DomainModelLayer.Repositories;
using ExpanseTrackerDDD.InfrastructureLayer.EF;
using Microsoft.Extensions.DependencyInjection;
using ExpanseTrackerDDD.ApplicationLayer.Commands.TransactionCommands;
using BaseDDD.DomainModelLayer.Events;
using RC_AL_IEH = ReportCreator.ApplicationLayer.IntegrationEventHandlers;
using RC_DML_I = ReportCreator.DomainModelLayer.Interfaces;
using ReportCreator.InfrastructureLayer.EF;
using RC_DML_R = ReportCreator.DomainModelLayer.Repositories;
using ET_DML_E = ExpanseTrackerDDD.DomainModelLayer.Events;
using ET_AL_DEH = ExpanseTrackerDDD.ApplicationLayer.DomainEventHandlers;
using RC_DML_E_IE = ReportCreator.DomainModelLayer.Events.IntegrationEvents;
using ET_DML_E_IE = ExpanseTrackerDDD.DomainModelLayer.Events.IntegrationEvents;
using BaseDDD.DomainModelLayer.Models;
using ReportCreator.ApplicationLayer.Commands.Handlers;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Users\AnnaSzmit\Documents\My project\Program\ExpanseTrackerTester\bin\Debug\netcoreapp3.1";
            string[] ETfileNames = { "ExpanseTrackerDDD_Base.db", "ExpanseTrackerDDD_Base.db-shm", "ExpanseTrackerDDD_Base.db-wal" };
            string[] CRfileNames = { "ReportCreatorDDD_Base.db", "ReportCreatorDDD_Base.db-shm", "ReportCreatorDDD_Base.db-wal" };

            for (int i = 0; i < ETfileNames.Length; i++)
            {
                if (File.Exists(Path.Combine(path, ETfileNames[i])))
                {
                    File.Delete(Path.Combine(path, ETfileNames[i]));
                }
            }


            //Konfiguracja Kontenera
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            //Przy każdym włączeniu aplikacji wykona się komenda odnawiająca budżet dla każdego zarejestrowanego użytkownika
            RenewAllBudgets(serviceCollection);
            SettleTransactions(serviceCollection);
            //MapEvents();

            //Testy
            var testCases = new TestCases(serviceCollection);
            testCases.Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            // event publishers and handlers - czy w tym podejściu tak trzeba robić?
            serviceCollection.AddSingleton<IDomainEventDispatcher>();
            serviceCollection.AddSingleton<IDomainEventHandler<ET_DML_E.TransactionUpdatedEvent>, ET_AL_DEH.TransactionUpdatedEventHandler>();
            serviceCollection.AddSingleton<IDomainEventHandler<ET_DML_E.TransactionCreatedEvent>, ET_AL_DEH.TransactionCreatedEventHandler>();

            serviceCollection.AddSingleton<IEventHandler<RC_DML_E_IE.UserCreatedEvent, ET_DML_E_IE.UserCreatedEvent>, RC_AL_IEH.UserCreatedEventHandler>();
            serviceCollection.AddSingleton<IEventHandler<RC_DML_E_IE.UserLoggedInEvent, ET_DML_E_IE.UserLoggedInEvent>, RC_AL_IEH.UserLoggedInEventHandler>();
            serviceCollection.AddSingleton<IEventHandler<RC_DML_E_IE.UserLoggedOutEvent, ET_DML_E_IE.UserLoggedOutEvent>, RC_AL_IEH.UserLoggedOutEventHandler>();

            serviceCollection.AddSingleton<IEventHandler<RC_DML_E_IE.AccountCreatedEvent, ET_DML_E_IE.AccountCreatedEvent>, RC_AL_IEH.AccountCreatedEventHandler>();
            serviceCollection.AddSingleton<IEventHandler<RC_DML_E_IE.AccountUpdatedEvent, ET_DML_E_IE.AccountUpdatedEvent>, RC_AL_IEH.AccountUpdatedEventHandler>();

            serviceCollection.AddSingleton<IEventHandler<RC_DML_E_IE.TransactionCreatedEvent, ET_DML_E.TransactionCreatedEvent>, RC_AL_IEH.TransactionCreatedEventHandler>();
            serviceCollection.AddSingleton<IEventHandler<RC_DML_E_IE.TransactionUpdatedEvent, ET_DML_E.TransactionUpdatedEvent>, RC_AL_IEH.TransactionUpdatedEventHandler>();


            //ETContext
            var ETContext = ETConnection.InitializeExpanseTrackerContext();
            serviceCollection.AddSingleton(ETContext);
            //RCContext
            var RCContext = ETConnection.InitializeExpanseTrackerContext();
            serviceCollection.AddSingleton(RCContext);

            //Commands and Query Handlers - Expanse Tracker
            serviceCollection.AddSingleton<UserCommandHandler>();
            serviceCollection.AddSingleton<AccountCommandHandler>();
            serviceCollection.AddSingleton<TransactionCommandHandler>();
            serviceCollection.AddSingleton<BudgetCommandHandler>();
            serviceCollection.AddSingleton<QueryHandler>();

            //Commands and Query Handlers - Report Creator
            serviceCollection.AddSingleton<ReportCommandHandler>();

            //Event Bus
            serviceCollection.AddSingleton<EventBus>();

            //UoW
            serviceCollection.AddSingleton<ET_DML_I.IExpanseTrackerUnitOfWork, ExpanseTrackerUnitOfWork>();
            serviceCollection.AddSingleton<RC_DML_I.IReportCreatorUnitOfWork, ReportCreatorUnitOfWork>();

            //Repositories
            serviceCollection.AddSingleton<ET_DML_I.IAccountRepository, ET_DML_R.AccountRepository>();
            serviceCollection.AddSingleton<ET_DML_I.IBudgetRepository, ET_DML_R.BudgetRepository>();
            serviceCollection.AddSingleton<ET_DML_I.ITransactionRepository, ET_DML_R.TransactionRepository>();
            serviceCollection.AddSingleton<ET_DML_I.IUserRepository, ET_DML_R.UserRepository>();

            serviceCollection.AddSingleton<RC_DML_I.IAccountRepository, RC_DML_R.AccountRepository>();
            serviceCollection.AddSingleton<RC_DML_I.ITransactionRepository, RC_DML_R.TransactionRepository>();

            // Domain Model services + factories + etc
            serviceCollection.AddSingleton<AccountFactory>();
            serviceCollection.AddSingleton<BudgetFactory>();
            serviceCollection.AddSingleton<TransactionFactory>();

            serviceCollection.AddSingleton<AccountMapper>();
            serviceCollection.AddSingleton<BudgetMapper>();
            serviceCollection.AddSingleton<Mappers>();
            serviceCollection.AddSingleton<TransactionMapper>();
            serviceCollection.AddSingleton<UserMapper>();
        }

        private static void RenewAllBudgets(IServiceCollection serviceCollection)
        {
            //Przy każdym włączeniu aplikacji wykona się komenda odnawiająca budżet dla każdego zarejestrowanego użytkownika
            IServiceProvider provider = serviceCollection.BuildServiceProvider();
            QueryHandler qh = provider.GetRequiredService<QueryHandler>();
            BudgetCommandHandler bch = provider.GetRequiredService<BudgetCommandHandler>();

            List<UserDto> users = qh.Execute(new GetAllUsersQuery());
            foreach(var u in users)
            {
                bch.Execute(new RenewBudgetCommand() { userId = u.Id });
            }
        }

        private static void SettleTransactions(IServiceCollection serviceCollection)
        {
            //Przy każdym włączeniu aplikacji wykona się komenda rozliczająca transakcje, których termin już minął
            IServiceProvider provider = serviceCollection.BuildServiceProvider();
            QueryHandler qh = provider.GetRequiredService<QueryHandler>();
            TransactionCommandHandler tch = provider.GetRequiredService<TransactionCommandHandler>();

            List<TransactionDto> transactions = qh.Execute(new GetAllUnsettledTransactionsQuery());
            foreach (var t in transactions)
            {
                tch.Execute(new SettleAllRequiredTransactions() { Id = t.Id });
            }
        }


    }
}
