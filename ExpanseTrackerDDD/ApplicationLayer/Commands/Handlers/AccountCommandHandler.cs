using ExpanseTrackerDDD.ApplicationLayer.Commands.AccountCommands;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.Handlers
{
    public class AccountCommandHandler
    {
        private IExpanseTrackerUnitOfWork _unitOfWork;

        public AccountCommandHandler(IExpanseTrackerUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Metoda tworząca konto
        /// </summary>
        /// <param name="command"></param>
        public void Execute(CreateAccountCommand command)
        {
            //Sprawdzenie, czy uźytkownik istnieje
            User user = this._unitOfWork.UserRepository.Get(command.UserId);
            if (user == null)
                throw new Exception($"User with Id '{command.UserId}' does not exist!");
            if (user.status != UserStatus.LoggedIn)
                throw new Exception("Please log in to create the account");

            //Sprawdzenie, czy konto może zostać utworzone
            Account account = this._unitOfWork.AccountRepository.Get(command.Id);
            if (account != null)
                throw new Exception($"Account with Id '{command.Id}' already exists!");
            account = this._unitOfWork.AccountRepository.GetAccountByName(command.Name);
            if (account != null)
                throw new Exception($"Account with name '{command.Name}' already exists!");
            account = this._unitOfWork.AccountRepository.GetAccountByNumber(command.AccountNumber);
            if (account != null)
                throw new Exception($"Account with number '{command.AccountNumber}' already exists!");

            //Utworzenie konta
            account = new Account(command.Id, command.Name, command.AccountNumber, command.Type, command.CurrencyName, command.UserId);
            this._unitOfWork.AccountRepository.Insert(account);

            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Metoda aktualizująca konto
        /// </summary>
        /// <param name="command"></param>
        public void Execute(UpdateAccount command)
        {
            //Sprawdzenie, czy konto istnieje
            Account account = this._unitOfWork.AccountRepository.Get(command.Id);
            if (account == null)
                throw new Exception($"Account with Id '{command.Id}' does not exist!");

            //Sprawdzenie, czy użytkownik jest zalogowany
            User user = this._unitOfWork.UserRepository.Get(account.UserId);
            if (user.status != UserStatus.LoggedIn)
                throw new Exception("Please log in to update the account");
            
            //Aktualizacja poszczególnych elementów
            if (command.Name != account.Name)
                account.UpdateName(command.Name);
            if (command.AccountNumber != account.AccountNumber)
                account.UpdateAccountNumber(command.AccountNumber);
            if (command.Type != account.Type)
                account.UpdateType(command.Type);
            
            //Aktualizacja bazy
            this._unitOfWork.AccountRepository.Update(account);

            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Usunięcie konta
        /// </summary>
        /// <param name="command"></param>
        public void Execute(DeleteAccountCommand command)
        {
            //Sprawdzenie, czy konto istnieje
            Account account = this._unitOfWork.AccountRepository.Get(command.Id);
            if (account == null)
                throw new Exception($"Account with Id '{command.Id}' does not exist!");

            //Sprawdzenie, czy użytkownik jest zalogowany
            User user = this._unitOfWork.UserRepository.Get(account.UserId);
            if (user.status != UserStatus.LoggedIn)
                throw new Exception("Please log in to delete the account");

            //Usunięcie konta
            this._unitOfWork.AccountRepository.Delete(account);

            this._unitOfWork.Commit();
        }

    }
}
