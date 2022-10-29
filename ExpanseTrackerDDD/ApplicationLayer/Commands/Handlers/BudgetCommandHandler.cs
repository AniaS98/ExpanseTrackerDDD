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

        public BudgetCommandHandler(IExpanseTrackerUnitOfWork unitOfWork, AccountFactory accountFactory, BudgetFactory budgetFactory, TransactionFactory transactionFactory)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Metoda służąca do utworzenia budżetu dla danego konta
        /// </summary>
        /// <param name="command"></param>
        public void Execute(CreateBudgetCommand command)
        {
            //Sprawdzenie, czy konto istnieje
            Account account = this._unitOfWork.AccountRepository.Get(command.AccountId);
            if (account == null)
                throw new Exception($"Account with Id '{command.AccountId}' does not exist!");
            
            //Sprawdzenie czy waluta budżetu zgadza się z walutą konta
            if (account.CurrencyName != command.LimitCurrency)
                throw new Exception($"Attempt to use different currency than the currency of the account ('{account.CurrencyName}').");
            
            //Stworzenie obiektu Money
            Money limit = new Money(command.LimitAmount, command.LimitCurrency);

            //Zgromadzenie kategorii obowiązujących w budżecie
            Category category = new Category(command.CategoryName, command.SubcategoryName);

            //Stworzenie budżetu
            Budget budget = new Budget(command.Id, command.Name, limit, command.Type, category, command.AccountId);
            this._unitOfWork.BudgetRepository.Insert(budget);

            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Metoda odnawiająca limit budżetu, wywoływana pierwszego dnia w danym miesiącu, w którym to dniu używano aplikacji
        /// </summary>
        /// <param name="command"></param>
        public void Execute(RenewBudgetCommand command)
        {
            //Sprawdzenie czy użytkownik istnieje
            User user = this._unitOfWork.UserRepository.Get(command.userId);
            if (user == null)
                throw new Exception($"User with Id '{command.userId}' does not exist");

            //Zebranie wszystkich kont użytkownika
            List<Account> accounts = this._unitOfWork.AccountRepository.GetAllByUserId(command.userId);
            foreach(var a in accounts)
            {
                //Dla każdego konta gromadzone są obecnie trwające budżety
                List<Budget> budgets = this._unitOfWork.BudgetRepository.GetAllByAccountId(a.Id);
                foreach(var b in budgets)
                {
                    //Jeżeli dany budżet jest aktywny, a czas jego trwania minął następuje deaktywacja budżetu i utworzenie nowego
                    if(b.EndTime<DateTime.Now && b.CurrentStatus == BudgetStatus.Active)
                    {
                        b.DeactivateBudget();
                        this._unitOfWork.BudgetRepository.Update(b);

                        if(b.Type == BudgetType.Monthly)
                        {
                            Budget budget = new Budget(new Guid(),  b.Name, b.Limit, BudgetType.Monthly, b.BudgetCategory, b.AccountId);
                            this._unitOfWork.BudgetRepository.Insert(budget);
                        }
                    }
                }
            }

            this._unitOfWork.Commit();
        }

    }
}
