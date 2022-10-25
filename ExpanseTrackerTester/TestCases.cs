using ExpanseTrackerDDD.ApplicationLayer.Commands.AccountCommands;
using ExpanseTrackerDDD.ApplicationLayer.Commands.Handlers;
using ExpanseTrackerDDD.ApplicationLayer.Commands.TransactionCommands;
using ExpanseTrackerDDD.ApplicationLayer.Commands.UserCommands;
using ExpanseTrackerDDD.ApplicationLayer.Queries;
using ExpanseTrackerDDD.ApplicationLayer.Queries.Handlers;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Tester
{
    public class TestCases
    {
        private IServiceProvider _serviceProvider;

        private UserCommandHandler _userCommandHandler;
        private AccountCommandHandler _accountCommandHandler;
        private BudgetCommandHandler _budgetCommandHandler;
        private TransactionCommandHandler _transactionCommandHandler;
        private QueryHandler _queryHandler;

        public TestCases(IServiceCollection serviceCollection)
        {
            _serviceProvider = serviceCollection.BuildServiceProvider();

            _userCommandHandler = _serviceProvider.GetRequiredService<UserCommandHandler>();
            _accountCommandHandler = _serviceProvider.GetRequiredService<AccountCommandHandler>();
            _budgetCommandHandler = _serviceProvider.GetRequiredService<BudgetCommandHandler>();
            _transactionCommandHandler = _serviceProvider.GetRequiredService<TransactionCommandHandler>();

            _queryHandler = _serviceProvider.GetRequiredService<QueryHandler>();
        }

        public void Run()
        {
            //Tworzenie użytkownika
            //hasło
            string password = "asd"; // new SecureString();
            string pas = "admin123";
            //foreach (char c in pas)
            //  password.AppendChar(c);
            //ID
            Guid userId = Guid.NewGuid();

            _userCommandHandler.Execute(new CreateUserCommand()
            {
                Id = userId,
                FirstName = "Krzysztof",
                LastName = "Pazór",
                Login = "kPaz",
                Password = password,
                RepeatPassword = password
            });
            Console.WriteLine("Account created");

            var users = _queryHandler.Execute(new GetAllUsersQuery());
            Console.WriteLine(users);
            //Tworzenie 2 kont
            Guid a1 = Guid.NewGuid();
            _accountCommandHandler.Execute(new CreateAccountCommand()
            {
                Id = a1,
                AccountNumber = "123",
                Color = "green",
                CurrencyName = CurrencyName.PLN,
                Name = "PKO",
                Type = AccountType.BankAccount,
                UserId = userId
            });

            Guid a2 = Guid.NewGuid();
            _accountCommandHandler.Execute(new CreateAccountCommand()
            {
                Id = a2,
                AccountNumber = "456",
                Color = "yellow",
                CurrencyName = CurrencyName.GBP,
                Name = "HSBC",
                Type = AccountType.BankAccount,
                UserId = userId
            });

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
                AccountId = a1
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
                AccountId = a1
            });






        }





    }
}
