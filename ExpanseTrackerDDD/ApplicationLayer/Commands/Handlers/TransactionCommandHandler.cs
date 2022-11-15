using BaseDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.ApplicationLayer.Commands.TransactionCommands;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Factories;
using ExpanseTrackerDDD.DomainModelLayer.Helpers;
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

        /// <summary>
        /// Metoda tworząca tranfer z jednego konta na drugie
        /// </summary>
        /// <param name="command"></param>
        public void Execute(CreateTransferCommand command)
        {
            //Zebranie obu kont
            Account fromAccount = this._unitOfWork.AccountRepository.Get(command.FromAccountId);
            Account toAccount = this._unitOfWork.AccountRepository.Get(command.ToAccountId);
            if (fromAccount.UserId != toAccount.UserId)
                throw new Exception("Unable to create transfer between those accounts!");

            //Sprawdzenie, czy użytkownik jest zalogowany
            User user = this._unitOfWork.UserRepository.Get(fromAccount.UserId);
            if (user.status != UserStatus.LoggedIn)
                throw new Exception("Please log in to create the transfer");

            //Stworzenie obiektu Money
            Money value = new Money(command.Amount, command.Currency);

            //Sprawdzenie stanu konta
           // AccountHelper.VerifyAccountBalance(fromAccount, value);

            //Sprawdzenie czy transfer może zostać wykonany
            if (fromAccount == null)
                throw new Exception($"Account with Id '{command.FromAccountId}' does not exist!");
            if (toAccount == null)
                throw new Exception($"Account with Id '{command.ToAccountId}' does not exist!");
            if (fromAccount.CurrencyName != command.Currency)
                throw new Exception($"Attempt to use different currency than the currency of the account ('{fromAccount.CurrencyName}').");

            Recurrency recurrency = new Recurrency(command.TransactionDate);
            //Stworzenie transakcji
            Transaction fromTransaction = _transactionFactory.CreateTransaction(command.FromTransactionId, command.Description, TransactionType.Transfer, -value, command.CatName, 
                command.CatSubcategoryName, recurrency, command.TransactionDate, command.Status, command.FromAccountId, "", command.Note);
            this._unitOfWork.TransactionRepository.Insert(fromTransaction);

            //Stworzenie zdarzenia informującego o stworzeniu transkacji
            fromTransaction.AddDomainEvent(new TransactionCreatedEvent(fromTransaction, fromAccount, null));

            //Transfer - stworzenie nowej tranakcji dla konta docelowego
            Transaction toTransaction = _transactionFactory.CreateTransfer(fromTransaction, command.ToAccountId);
            this._unitOfWork.TransactionRepository.Insert(toTransaction);

            //Stworzenie zdarzenia informującego o stworzeniu transkacji
            toTransaction.AddDomainEvent(new TransactionCreatedEvent(toTransaction, toAccount, null));
            //toTransaction.AddIntegrationEvent(new RC_TransactionCreatedEvent(toTransaction, toAccount));

            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Metoda tworząca transfer z jednego konta na drugie o innej walucie
        /// </summary>
        /// <param name="command"></param>
        public void Execute(CreateTransferWithExchangeCommand command)
        {
            //Zebranie obu kont
            Account fromAccount = this._unitOfWork.AccountRepository.Get(command.FromAccountId);
            Account toAccount = this._unitOfWork.AccountRepository.Get(command.ToAccountId);
            if (fromAccount.UserId != toAccount.UserId)
                throw new Exception("Unable to create transfer between those accounts!");

            //Sprawdzenie, czy użytkownik jest zalogowany
            User user = this._unitOfWork.UserRepository.Get(fromAccount.UserId);
            if (user.status != UserStatus.LoggedIn)
                throw new Exception("Please log in to create the transfer");

            //Stworzenie obiektu Money
            Money value = new Money(command.Amount, command.Currency);

            //Sprawdzenie stanu konta
            //AccountHelper.VerifyAccountBalance(fromAccount, value);

            //Sprawdzenie czy transfer może zostać wykonany
            if (fromAccount == null)
                throw new Exception($"Account with Id '{command.FromAccountId}' does not exist!");
            if (toAccount == null)
                throw new Exception($"Account with Id '{command.ToAccountId}' does not exist!");
            if (fromAccount.CurrencyName != command.Currency)
                throw new Exception($"Attempt to use different currency than the currency of the account ('{fromAccount.CurrencyName}').");

            Recurrency recurrency = new Recurrency(command.TransactionDate);
            //Stworzenie transakcji
            Transaction fromTransaction = _transactionFactory.CreateTransaction(command.FromTransactionId, command.Description, 
                TransactionType.Transfer, -value, command.CatName, command.CatSubcategoryName, recurrency, command.TransactionDate, 
                command.Status, command.FromAccountId, "", command.Note);
            this._unitOfWork.TransactionRepository.Insert(fromTransaction);

            //Stworzenie zdarzenia informującego o stworzeniu transkacji
            fromTransaction.AddDomainEvent(new TransactionCreatedEvent(fromTransaction, fromAccount, null));

            //Wymiana - stworzenie nowej tranakcji dla konta docelowego
            Transaction toTransaction = _transactionFactory.Exchange(fromTransaction, toAccount.Id, toAccount.CurrencyName);
            this._unitOfWork.TransactionRepository.Insert(toTransaction);

            //Stworzenie zdarzenia informującego o stworzeniu transkacji
            toTransaction.AddDomainEvent(new TransactionCreatedEvent(toTransaction, toAccount, null));

            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Metoda tworząca transakcję
        /// </summary>
        /// <param name="command"></param>
        public void Execute(CreateTransactionCommand command)
        {
            //Pobranie konta
            Account account = this._unitOfWork.AccountRepository.Get(command.AccountId);

            // Pobranie zmiennej Budzet
            Budget budget = this._unitOfWork.BudgetRepository.GetActiveByAccountIdAndCategory(command.AccountId, new Category(command.CatName, command.CatSubcategoryName));

            //Sprawdzenie, czy użytkownik jest zalogowany
            User user = this._unitOfWork.UserRepository.Get(account.UserId);
            if (user.status != UserStatus.LoggedIn)
                throw new Exception("Please log in to create the transaction");

            //Stworzenie obiektu Money
            Money value = new Money(command.Amount, command.Currency);

            //Sprawdzenie stanu konta
            //AccountHelper.VerifyAccountBalance(account, value);

            //Sprawdzenie czy transakcja może zostać wykonana
            if (account == null)
                throw new Exception($"Account with Id '{command.AccountId}' does not exist!");
            if (account.CurrencyName != command.Currency)
                throw new Exception($"Attempt to use different currency than the currency of the account ('{account.CurrencyName}').");

            //Ustawienei Recurrency
            Recurrency recurrency = RecurrencyHelper.SetRecurrency(command.RecurType, command.NumberOfRecurrencies, command.EndDate, command.Period, command.DayOfTheMonth, command.DaysApart);

            DateTime date = command.TransactionDate;
            for (int i=0; i< recurrency.NumberOfRecurrencies; i++)
            {
                //Stworzenie transakcji
                Transaction transaction = _transactionFactory.CreateTransaction(command.Id, command.Description, TransactionType.Exchange, -value, command.CatName, command.CatSubcategoryName,
                    recurrency, date, command.Status, command.AccountId, "", command.Note);
                this._unitOfWork.TransactionRepository.Insert(transaction);

                date.AddDays(recurrency.DaysApart);
                //Stworzenie zdarzenia informującego o stworzeniu transkacji
                if(i == 0)
                    transaction.AddEvent(new TransactionCreatedEvent(transaction, account, budget));
            }

            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Metoda aktualizująca transakcję
        /// </summary>
        /// <param name="command"></param>
        public void Execute(UpdateTransactionCommand command)
        {
            //Pobieranie konta i transakcji 
            Account account = this._unitOfWork.AccountRepository.Get(command.AccountId);
            Transaction transaction = this._unitOfWork.TransactionRepository.Get(command.Id);

            // Pobranie zmiennej Budzet
            Budget budget = this._unitOfWork.BudgetRepository.GetActiveByAccountIdAndCategory(command.AccountId, transaction.TransactionCategory);

            //Sprawdzenie, czy użytkownik jest zalogowany
            User user = this._unitOfWork.UserRepository.Get(account.UserId);
            if (user.status != UserStatus.LoggedIn)
                throw new Exception("Please log in to update the transaction");

            //Sprawdzenie czy transakcja może zostać wykonana
            if (transaction == null)
                throw new Exception($"Transaction '{command.Id}' could not be found!");            
            if (account == null)
                throw new Exception($"Account with Id '{command.AccountId}' does not exist!");
            if (account.CurrencyName != command.Currency)
                throw new Exception($"Attempt to use different currency than the currency of the account ('{account.CurrencyName}').");

            //Stworzenie obiektu Money
            Money value = new Money(command.Amount, command.Currency);

            //Wprowadzenie zmian na transakcji
            transaction = _transactionFactory.UpdateTransaction(transaction, command.Description, value, command.CatName, command.CatSubcategoryName, command.RecurType, 
                command.NumberOfRecurrencies, command.EndDate, command.Period, command.DayOfTheMonth, command.DaysApart, command.TransactionDate, command.Status, 
                command.AccountId, command.Contractor, command.Note);
            this._unitOfWork.TransactionRepository.Update(transaction);

            //Stworzenie zdarzenia informującego o zaktualizowaniu transkacji
            transaction.AddEvent(new TransactionUpdatedEvent(transaction, account, budget, value));

            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Usunięcie transakcji
        /// </summary>
        /// <param name="command"></param>
        public void Execute(DeleteTransactionCommand command)
        {
            //Wyszukanie transakcji ze sprawdzenie czy istnieje
            Transaction transaction = this._unitOfWork.TransactionRepository.Get(command.FirstTransactionId);
            if (transaction == null)
                throw new Exception($"Transaction '{command.FirstTransactionId}' could not be found!");

            //Wyszukanie konta ze srawdzeniem czy istnieje
            Account account = this._unitOfWork.AccountRepository.Get(transaction.AccountId);
            if (account == null)
                throw new Exception($"Account with Id '{transaction.AccountId}' does not exist!");

            //Sprawdzenie, czy użytkownik jest zalogowany
            User user = this._unitOfWork.UserRepository.Get(account.UserId);
            if (user.status != UserStatus.LoggedIn)
                throw new Exception("Please log in to delete the transaction");

            // Pobranie zmiennej Budzet
            Budget budget = this._unitOfWork.BudgetRepository.GetActiveByAccountIdAndCategory(account.Id, transaction.TransactionCategory);

            //Stworzenie obiektu Money z atrybutem Amount rónwym 0
            Money value = new Money(account.CurrencyName);

            //Stworzenie zdarzenia informującego o zaktualizowaniu transkacji
            transaction.AddDomainEvent(new TransactionUpdatedEvent(transaction, account, budget, value));

            //Usunięcie transakcji
            this._unitOfWork.TransactionRepository.Delete(transaction);

            //Jeżeli transakcja była typu Transfer lub exchange należy również usunąć drugą transakcję
            if(transaction.Type == TransactionType.Exchange || transaction.Type == TransactionType.Transfer)
            {
                try
                {
                    //Sprawdzenie czy druga transakcja moze zostac usunięta wraz z wyszukaniem tej transakcji
                    if(command.SecondTransactionId == null)
                        throw new Exception($"Could not delete transfer, pleasse provide the Id for the second transaction.");
                    transaction = this._unitOfWork.TransactionRepository.Get(command.SecondTransactionId);
                    if (transaction == null)
                        throw new Exception($"Transaction '{command.FirstTransactionId}' could not be found!");

                    //Wyszukanie konta ze srawdzeniem czy istnieje
                    account = this._unitOfWork.AccountRepository.Get(transaction.AccountId);
                    if (account == null)
                        throw new Exception($"Account with Id '{transaction.AccountId}' does not exist!");
                }
                catch(Exception e)
                {
                    this._unitOfWork.RejectChanges();
                    throw e;
                }

                //Stworzenie zdarzenia informującego o zaktualizowaniu transkacji
                transaction.AddEvent(new TransactionUpdatedEvent(transaction, account, budget, value));

                //Usunięcie transakcji
                this._unitOfWork.TransactionRepository.Delete(transaction);
            }

            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Metoda rozliczająca transakcję, jeżeli minął czas jej realizacji
        /// </summary>
        /// <param name="command"></param>
        public void Execute(SettleAllRequiredTransactions command)
        {
            //Wyszukanie transakcji ze sprawdzenie czy istnieje
            Transaction transaction = this._unitOfWork.TransactionRepository.Get(command.Id);
            if (transaction == null)
                throw new Exception($"Transaction '{command.Id}' could not be found!");

            //Wyszukanie konta ze srawdzeniem czy istnieje
            Account account = this._unitOfWork.AccountRepository.Get(transaction.AccountId);
            if (account == null)
                throw new Exception($"Account with Id '{transaction.AccountId}' does not exist!");

            // Pobranie zmiennej Budzet
            Budget budget = this._unitOfWork.BudgetRepository.GetActiveByAccountIdAndCategory(account.Id, transaction.TransactionCategory);

            //Jeżeli data transakcji minęła (lub jest tego samego dnia) to status zmienia się na settled, następuje zgłoszenie zdarzenia
            if (transaction.TransactionDate <= DateTime.Now)
            {
                transaction.UpdateStatus(TransactionStatus.Settled);
                transaction.AddEvent(new TransactionCreatedEvent(transaction, account, budget));
                this._unitOfWork.TransactionRepository.Update(transaction);
            }
            this._unitOfWork.Commit();
        }







        //Osobne metody do recurring albo planned






    }
}
