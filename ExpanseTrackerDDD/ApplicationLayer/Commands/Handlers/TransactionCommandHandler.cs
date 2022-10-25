using ExpanseTrackerDDD.ApplicationLayer.Commands.TransactionCommands;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Factories;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.Handlers
{
    public class TransactionCommandHandler
    {
        private IExpanseTrackerUnitOfWork _unitOfWork;
        private TransactionFactory _transactionFactory;
        

        public TransactionCommandHandler(IExpanseTrackerUnitOfWork unitOfWork, TransactionFactory transactionFactory)
        {
            _unitOfWork = unitOfWork;
            _transactionFactory = transactionFactory;
        }

        public void Execute(CreateTransactionCommand command)
        {
            Account account = this._unitOfWork.AccountRepository.Get(command.AccountId);
            if (account == null)
                throw new Exception($"Account with Id '{command.AccountId}' does not exist!");

            if (account.CurrencyName != command.Currency)
                throw new Exception($"Attempt to use different currency than the currency of the account ('{account.CurrencyName}').");
            Money money = new Money(command.Amount, command.Currency);

            Transaction transaction = _transactionFactory.CreateTransaction(command.Id, command.Description, command.TransactionType, money, command.CatName, command.CatSubcategoryName, command.RecurType, command.NumberOfRecurrencies, command.EndDate, command.Period, command.DayOfTheMonth, command.DaysApart, command.TransactionDate, command.Status, command.AccountId, command.Contractor, command.Note);

            this._unitOfWork.TransactionRepository.Insert(transaction);

            if (command.TransactionType == TransactionType.Transfer)
            {
                transaction = _transactionFactory.CreateTransfer(transaction, command.DestinationAccountId);
                this._unitOfWork.TransactionRepository.Insert(transaction);
            }
            else if(command.TransactionType == TransactionType.Exchange)//Exchange można dokonać jedynie na inne konto użytkownika, jest to więc transfer, na którym dodatkowo zmienia się waluta, metoda zaproponuje kurs waluty, jeżeli użytkownik zdecyduje, że kwota w docelowej walucie jest inna po prostu dokona zmiany w transakcji
            {
                transaction = _transactionFactory.Exchange(transaction, command.DestinationAccountId, command.newCurrency);
                this._unitOfWork.TransactionRepository.Insert(transaction);
            }

            this._unitOfWork.Commit();
        }

        public void Execute(UpdateTransactionCommand command)
        {
            Transaction transaction = this._unitOfWork.TransactionRepository.Get(command.Id);
            if (transaction == null)
                throw new Exception($"Transaction '{command.Id}' could not be found!");

            Account account = this._unitOfWork.AccountRepository.Get(command.AccountId);
            if (account == null)
                throw new Exception($"Account with Id '{command.AccountId}' does not exist!");

            if (account.CurrencyName != command.Currency)
                throw new Exception($"Attempt to use different currency than the currency of the account ('{account.CurrencyName}').");
            Money money = new Money(command.Amount, command.Currency);

            transaction = _transactionFactory.CreateTransaction(command.Id, command.Description, command.TransactionType, money, command.CatName, command.CatSubcategoryName, command.RecurType, command.NumberOfRecurrencies, command.EndDate, command.Period, command.DayOfTheMonth, command.DaysApart, command.TransactionDate, command.Status, command.AccountId, command.Contractor, command.Note);
            this._unitOfWork.TransactionRepository.Update(transaction);                
            this._unitOfWork.Commit();
        }

    }
}
