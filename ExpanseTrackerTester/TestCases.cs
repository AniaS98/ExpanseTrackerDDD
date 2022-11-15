using BaseDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.ApplicationLayer.Commands.AccountCommands;
using ExpanseTrackerDDD.ApplicationLayer.Commands.Handlers;
using ExpanseTrackerDDD.ApplicationLayer.Commands.TransactionCommands;
using ExpanseTrackerDDD.ApplicationLayer.Commands.UserCommands;
using ExpanseTrackerDDD.ApplicationLayer.Queries;
using ExpanseTrackerDDD.ApplicationLayer.Queries.Handlers;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.InfrastructureLayer.EF;
using Microsoft.Extensions.DependencyInjection;
using ReportCreator.ApplicationLayer.Commands;
using ReportCreator.ApplicationLayer.Commands.Handlers;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Tester
{
    public class TestCases
    {
        private IServiceProvider _serviceProvider;

        //Expanse Tracker
        private UserCommandHandler _userCommandHandler;
        private AccountCommandHandler _accountCommandHandler;
        private BudgetCommandHandler _budgetCommandHandler;
        private TransactionCommandHandler _transactionCommandHandler;
        private QueryHandler _queryHandler;

        //Report Creator
        private ReportCommandHandler _reportCommandHandler;

        private EventBus _eventBus;

        public TestCases(IServiceCollection serviceCollection)
        {
            _serviceProvider = serviceCollection.BuildServiceProvider();
            
            _userCommandHandler = _serviceProvider.GetRequiredService<UserCommandHandler>();
            _accountCommandHandler = _serviceProvider.GetRequiredService<AccountCommandHandler>();
            _budgetCommandHandler = _serviceProvider.GetRequiredService<BudgetCommandHandler>();
            _transactionCommandHandler = _serviceProvider.GetRequiredService<TransactionCommandHandler>();
            _reportCommandHandler = _serviceProvider.GetRequiredService<ReportCommandHandler>();

            _queryHandler = _serviceProvider.GetRequiredService<QueryHandler>();

            _eventBus = _serviceProvider.GetRequiredService<EventBus>();
        }

        public EventBus GetEvents()
        {
            return _serviceProvider.GetRequiredService<ExpanseTrackerUnitOfWork>().EventBus;
        }

        public void Run()
        {
            //Tworzenie pierwszego użytkownika
            //hasło
            string password = "Pas@123"; // new SecureString();
            string repeatPassword = "Pas@123!";
            //foreach (char c in pas)
            //  password.AppendChar(c);
            //ID
            Guid userId1 = Guid.NewGuid();

            _userCommandHandler.Execute(new CreateUserCommand()
            {
                Id = userId1,
                FirstName = "Krzysztof",
                LastName = "Pazór",
                Login = "kPaz",
                Password = password,
                RepeatPassword = password
            });
            Console.WriteLine("User created\n");

            _reportCommandHandler.Execute(new ShowAllReportsCommand()
            {
                eventBus = GetEvents()
            }) ;


            //Wyświetlenie wszystkich użytkowników
            var users = _queryHandler.Execute(new GetAllUsersQuery());
            foreach(var i in users)
                Console.WriteLine(i.ToString());
            Console.WriteLine("\n");

            //Tworzenie drugiego użytkownika z niezgodnymi hasłami
            //hasło
            password = "Azor*789"; // new SecureString();
            Guid userId2 = Guid.NewGuid();
            try
            {
                _userCommandHandler.Execute(new CreateUserCommand()
                {
                    Id = userId2,
                    FirstName = "Julia",
                    LastName = "Kowalska",
                    Login = "jKow",
                    Password = password,
                    RepeatPassword = repeatPassword
                });
                Console.WriteLine("Account created\n");
            }
            catch(Exception e)
            {
                Console.WriteLine(String.Format("User could not be created, because of the exception:\n{0}\n", e.Message));
            }

            //Wyświetlenie wszystkich użytkowników
            users = _queryHandler.Execute(new GetAllUsersQuery());
            foreach (var i in users)
                Console.WriteLine(i.ToString());
            Console.WriteLine("\n");

            //Tworzenie drugiego użytkownika, druga próba
            //hasło
            password = "Azor*789"; // new SecureString();
            _userCommandHandler.Execute(new CreateUserCommand()
            {
                Id = userId2,
                FirstName = "Julia",
                LastName = "Kowalska",
                Login = "jKow",
                Password = password,
                RepeatPassword = password
            });
            Console.WriteLine("User created\n");

            //Wyświetlenie wszystkich użytkowników
            users = _queryHandler.Execute(new GetAllUsersQuery());
            foreach (var i in users)
                Console.WriteLine(i.ToString());
            Console.WriteLine("\n");


            //Usunięcie pierwszego użytkownika
            _userCommandHandler.Execute(new DeleteUserCommand()
            { Id = userId1 });
            Console.WriteLine(string.Format("User with id: {0} has been deleted\n", userId1));

            //Wyświetlenie wszystkich użytkowników
            users = _queryHandler.Execute(new GetAllUsersQuery());
            foreach (var i in users)
                Console.WriteLine(i.ToString());
            Console.WriteLine("\n");


            //Utworzenie pierwszego konta z PLN
            Guid accountId1 = Guid.NewGuid();

            _accountCommandHandler.Execute(new CreateAccountCommand()
            {
                Id = accountId1,
                AccountNumber = "123",
                CurrencyName = CurrencyName.PLN,
                Name = "PKO",
                Type = AccountType.BankAccount,
                UserId = userId2,
                BalanceValue = 0.00m
            });
            Console.WriteLine("Account created");

            //Wyświetlanie wszystkich kont drugiego użytkownika
            var accounts = _queryHandler.Execute(new GetAllAccountsOfUserQuery() { UserId = userId2 });
            foreach (var i in accounts)
                Console.WriteLine(i.ToString());
            Console.WriteLine("\n");

            //Utworzenie drugiego konta z PLN
            Guid accountId2 = Guid.NewGuid();

            _accountCommandHandler.Execute(new CreateAccountCommand()
            {
                Id = accountId2,
                AccountNumber = "456",
                CurrencyName = CurrencyName.PLN,
                Name = "mBank",
                Type = AccountType.BankAccountWithOverdraft,
                BalanceValue = 0.00m,
                OverdraftValue = 500m,
                UserId = userId2
            });
            Console.WriteLine("Account created");

            //Wyświetlanie wszystkich kont drugiego użytkownika
            accounts = _queryHandler.Execute(new GetAllAccountsOfUserQuery() { UserId = userId2 });
            foreach (var i in accounts)
                Console.WriteLine(i.ToString());
            Console.WriteLine("\n");


            //Utworzenie konta z EUR
            Guid accountId3 = Guid.NewGuid();

            _accountCommandHandler.Execute(new CreateAccountCommand()
            {
                Id = accountId3,
                AccountNumber = "456",
                CurrencyName = CurrencyName.EUR,
                Name = "HSBC",
                Type = AccountType.BankAccount,
                BalanceValue = 0.00m,
                UserId = userId2
            });
            Console.WriteLine("Account created");

            //Wyświetlanie wszystkich kont drugiego użytkownika
            accounts = _queryHandler.Execute(new GetAllAccountsOfUserQuery() { UserId = userId2 });
            foreach (var i in accounts)
                Console.WriteLine(i.ToString());
            Console.WriteLine("\n");




            //Przykładowe transakcje
            Guid t1 = Guid.NewGuid();
            _transactionCommandHandler.Execute(new CreateTransactionCommand()
            {
                Id = t1,
                Amount = 10.3m,
                Currency = CurrencyName.PLN,
                CatName = CategoryName.Food,
                CatSubcategoryName = SubcategoryName.Food_Groceries,
                Contractor = "Cafe Bageri Stockholm",
                Description = "ciastko",
                TransactionType = TransactionType.Expanse,
                TransactionDate = new DateTime(2022,9,1),
                Status = TransactionStatus.Settled,
                Note = "bardzo dobre ciastko",
                AccountId = accountId1
            });

            Guid t2 = Guid.NewGuid();
            _transactionCommandHandler.Execute(new CreateTransactionCommand()
            {
                Id = t2,
                Amount = 10.3m,
                Currency = CurrencyName.PLN,
                CatName = CategoryName.Food,
                CatSubcategoryName = SubcategoryName.Food_Groceries,
                Contractor = "Cafe Bageri Stockholm",
                Description = "ciastko",
                TransactionType = TransactionType.Expanse,
                TransactionDate = new DateTime(2022, 9, 1),
                Status = TransactionStatus.Settled,
                Note = "bardzo dobre ciastko",
                AccountId = accountId1
            });
            accounts = _queryHandler.Execute(new GetAllAccountsQuery());



            Console.WriteLine("test");

            _reportCommandHandler.Execute(new ShowAllReportsCommand() 
            { 
                eventBus = GetEvents(),
                ReportPeriod = ReportPeriod.Current,
                ReportType = ReportCreator.DomainModelLayer.Models.ReportType.Weekly,
                UserId = userId1
            });

            Console.WriteLine("test");
        }





    }
}
