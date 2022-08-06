using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.InfrastructureLayer
{
    public class ExpanseTrackerUnitOfWork : IExpanseTrackerUnitOfWork
    {
        public IRepository<User> UserRepository { get; }
        public IRepository<Account> AccountRepository { get; }
        public IRepository<Budget> BudgetRepository { get; }
        public IRepository<Transaction> TransactionRepository { get; }

        public ExpanseTrackerUnitOfWork(IRepository<User> userRepository, IRepository<Account> accountRepository, IRepository<Budget> budgetRepository, IRepository<Transaction> transactionRepository)
        {
            UserRepository = userRepository;
            AccountRepository = accountRepository;
            BudgetRepository = budgetRepository;
            TransactionRepository = transactionRepository;
        }

        public ExpanseTrackerUnitOfWork()
        {
            UserRepository = new Repository<User>();
            AccountRepository = new Repository<Account>();
            BudgetRepository = new Repository<Budget>();
            TransactionRepository = new Repository<Transaction>();
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public void RejectChanges()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
