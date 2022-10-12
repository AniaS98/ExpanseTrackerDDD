using System;
using System.Collections.Generic;
using ExpanseTrackerDDD.ApplicationLayer.Commands.BudgetCommands;
using ExpanseTrackerDDD.ApplicationLayer.Commands.Handlers;
using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.ApplicationLayer.Mappers;
using ExpanseTrackerDDD.ApplicationLayer.Queries;
using ExpanseTrackerDDD.ApplicationLayer.Queries.Handlers;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Factories;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Repositories;
using ExpanseTrackerDDD.DomainModelLayer.Services;
using ExpanseTrackerDDD.InfrastructureLayer.EF;
using Microsoft.Extensions.DependencyInjection;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            //Konfiguracja Kontenera
            IServiceCollection serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            //Przy każdym włączeniu aplikacji wykona się komenda odnawiająca budżet dla każdego zarejestrowanego użytkownika
            RenewAllBudgets(serviceCollection);

            //Testy
            var testCases = new TestCases(serviceCollection);
            testCases.Run();
        }

        private static void ConfigureServices(IServiceCollection serviceCollection)
        {
            //ETContext
            var context = ETConnection.InitializeExpanseTrackerContext();
            serviceCollection.AddSingleton(context);

            //Commands and Query Handlers
            serviceCollection.AddSingleton<UserCommandHandler>();
            serviceCollection.AddSingleton<AccountCommandHandler>();
            serviceCollection.AddSingleton<TransactionCommandHandler>();
            serviceCollection.AddSingleton<BudgetCommandHandler>();
            serviceCollection.AddSingleton<QueryHandler>();


            // event publishers
            serviceCollection.AddSingleton<IDomainEventPublisher>();

            //UoW + repos
            serviceCollection.AddSingleton<IExpanseTrackerUnitOfWork, ExpanseTrackerUnitOfWork>();
            serviceCollection.AddSingleton<IAccountRepository, AccountRepository>();
            serviceCollection.AddSingleton<IBudgetRepository, BudgetRepository>();
            serviceCollection.AddSingleton<ITransactionRepository, TransactionRepository>();
            serviceCollection.AddSingleton<IUserRepository, UserRepository>();

            // Domain Model services + factories + etc
            // na razie nie ma policy
            serviceCollection.AddSingleton<AccountFactory>();
            serviceCollection.AddSingleton<BudgetFactory>();
            serviceCollection.AddSingleton<TransactionFactory>();
            serviceCollection.AddSingleton<IDomainService>();//tu chyba jeszcze brakuje
            //Resztę serwisów przesunąć do Domeny???
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
    }
}
