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
        private IServiceProvider _serviceProvide;

        private UserCommandHandler _userCommandHandler;
        private AccountCommandHandler _accountCommandHandler;
        private BudgetCommandHandler _budgetCommandHandler;
        private TransactionCommandHandler _transactionCommandHandler;
        private QueryHandler _queryHandler;

        public TestCases(IServiceCollection serviceCollection)
        {
            _serviceProvide = serviceCollection.BuildServiceProvider();

            _userCommandHandler = _serviceProvide.GetRequiredService<UserCommandHandler>();
            _queryHandler = _serviceProvide.GetRequiredService<QueryHandler>();
        }

        public void Run()
        {
            //Tworzenie użytkownika
            //hasło
            SecureString password = new SecureString();
            string pas = "admin123";
            foreach (char c in pas)
                password.AppendChar(c);
            //ID
            Guid id = new Guid();

            _userCommandHandler.Execute(new CreateUserCommand()
            {
                Id = id,
                FirstName = "Krzysztof",
                LastName = "Pazór",
                Login = "kPaz",
                Password = password,
                RepeatPassword = password
            });
            Console.WriteLine("Account created");

            var users = _queryHandler.Execute(new GetAllUsersQuery());

            //Tworzenie 2 kont
            Guid pkoId = new Guid();
            _accountCommandHandler.Execute(new CreateAccountCommand()
            {
                Id = new Guid(),
                AccountNumber = "123",
                Color = "green",
                CurrencyName = CurrencyName.PLN,
                Name = "PKO",
                Type = AccountType.BankAccount,
                UserId = id
            });

            Guid hsbcId = new Guid();
            _accountCommandHandler.Execute(new CreateAccountCommand()
            {
                Id = new Guid(),
                AccountNumber = "456",
                Color = "yellow",
                CurrencyName = CurrencyName.GBP,
                Name = "HSBC",
                Type = AccountType.BankAccount,
                UserId = id
            });

            //Przykładowe transakcje
            _transactionCommandHandler.Execute(new CreateTransactionCommand()
            {
                AccountId = pkoId,
                Amount = 10.3m,
                Currency = CurrencyName.PLN,
                CatName = CategoryName.Food,
                CatSubcategoryName = SubcategoryName.Food_Groceries,
                Contractor = "Cafe Bageri Stockholm",
                Description = "ciastko",
                TransactionType = TransactionType.Expanse,
                TransactionDate = new DateTime(2022,9,1),
                Status = TransactionStatus.Settled,
                Note = "bardzo dobre ciastko"                
            });

            _transactionCommandHandler.Execute(new CreateTransactionCommand()
            {
                AccountId = pkoId,
                Amount = 10.3m,
                Currency = CurrencyName.PLN,
                CatName = CategoryName.Food,
                CatSubcategoryName = SubcategoryName.Food_Groceries,
                Contractor = "Cafe Bageri Stockholm",
                Description = "ciastko",
                TransactionType = TransactionType.Expanse,
                TransactionDate = new DateTime(2022, 9, 1),
                Status = TransactionStatus.Settled,
                Note = "bardzo dobre ciastko"
            });






        }





    }
}
