using ExpanseTrackerDDD.ApplicationLayer.Commands.BudgetCommands;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Factories;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.Handlers
{
    public class BudgetCommandHandler
    {
        private IExpanseTrackerUnitOfWork _unitOfWork;
        private AccountFactory _accountFactory;
        private BudgetFactory _budgetFactory;
        private TransactionFactory _transactionFactory;
        private IDomainEventPublisher _domainEventPublisher;

        public BudgetCommandHandler(IExpanseTrackerUnitOfWork unitOfWork, AccountFactory accountFactory, BudgetFactory budgetFactory, TransactionFactory transactionFactory, IDomainEventPublisher domainEventPublisher)
        {
            _unitOfWork = unitOfWork;
            _accountFactory = accountFactory;
            _budgetFactory = budgetFactory;
            _transactionFactory = transactionFactory;

            _domainEventPublisher = domainEventPublisher;
        }

        public void Execute(CreateBudgetCommand command)
        {
            Account account = this._unitOfWork.AccountRepository.Get(command.AccountId);
            if (account == null)
                throw new Exception($"Account with Id '{command.AccountId}' does not exist!");

            if (account.CurrencyName != command.LimitCurrency)
                throw new Exception($"Attempt to use different currency than the currency of the account ('{account.CurrencyName}').");
            
            Money limit = new Money(command.LimitAmount, command.LimitCurrency);

            List<Category> categories = new List<Category>();
            var lists = command.CategoryNames.Zip(command.SubcategoryNames, (n, s) => new { Name = n, SubName = s });
            foreach (var ns in lists)
            {
                Category cat = new Category(ns.Name, ns.SubName);
                categories.Add(cat);
            }

            Budget budget = new Budget(command.Id, _domainEventPublisher, command.Name, limit, command.Type, categories, command.AccountId);
            this._unitOfWork.BudgetRepository.Insert(budget);
            this._unitOfWork.Commit();
        }

        public void Execute(RenewBudgetCommand command)
        {
            User user = this._unitOfWork.UserRepository.Get(command.userId);
            if (user == null)
                throw new Exception($"User with Id '{command.userId}' does not exist");

            List<Account> accounts = this._unitOfWork.AccountRepository.GetAllByUserId(command.userId);
            foreach(var a in accounts)
            {
                List<Budget> budgets = this._unitOfWork.BudgetRepository.GetAllByAccountId(a.Id);
                foreach(var b in budgets)
                {
                    if(b.EndTime<DateTime.Now && b.CurrentStatus == BudgetStatus.Active)
                    {
                        b.DeactivateBudget();
                        this._unitOfWork.BudgetRepository.Update(b);

                        if(b.Type == BudgetType.Monthly)
                        {
                            Budget budget = new Budget(new Guid(), _domainEventPublisher, b.Name, b.Limit, BudgetType.Monthly, b.Categories, b.AccountId);
                            this._unitOfWork.BudgetRepository.Insert(budget);
                        }
                    }
                }
            }
            this._unitOfWork.Commit();
        }

    }
}
