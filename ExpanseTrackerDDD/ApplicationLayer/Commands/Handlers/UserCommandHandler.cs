using ExpanseTrackerDDD.ApplicationLayer.Commands.UserCommands;
using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Helpers;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.Handlers
{
    public class UserCommandHandler
    {
        private IExpanseTrackerUnitOfWork _unitOfWork;
        
        //wyczyścić konstruktor
        public UserCommandHandler(IExpanseTrackerUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Metoda tworząca nowy obiekt User
        /// </summary>
        /// <param name="command"></param>
        public void Execute(CreateUserCommand command)
        {
            //Sprawdzenie czy użytkownik istnieje
            User user = this._unitOfWork.UserRepository.Get(command.Id);
            if (user != null)
                throw new Exception($"User with Id '{command.Id}' already exists!");
            user = this._unitOfWork.UserRepository.GetUserByLogin(command.Login);
            if (user != null)
                throw new Exception($"User with login '{command.Login}' already exists!");

            //Sprawdzenie czy oba hasła spełniają wymagania oraz czy się zgadzają
            UserHelper.PasswordValidation(command.Password, command.RepeatPassword);

            //Utworzenie nowego użytkownika
            user = new User(command.Id,  command.Login, command.Password, command.FirstName, command.LastName);
            this._unitOfWork.UserRepository.Insert(user);

            //Utworzenie zdarzenia utworzenia użytkownika


            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Metoda służąca do logowania
        /// </summary>
        /// <param name="command"></param>
        public void Execute(LogInCommand command)
        {
            //Wyszukanie uźytkownika
            User user = _unitOfWork.UserRepository.GetUserByLogin(command.Login);
            if (user == null)
                throw new Exception("Incorrect login");

            //Weryfikacja czy podane hasło zgadza się z tym przypisanym do uźytkownika
            UserHelper.PasswordVerification(user.Password, command.Password);

            //Logowanie
            user.LogIn();
            this._unitOfWork.UserRepository.Update(user);

            //Utworzenie zdarzenia odnośnie logowania do konta


            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Metoda służąca do logowania
        /// </summary>
        /// <param name="command"></param>
        public void Execute(LogOutCommand command)
        {
            //Wyszukanie uźytkownika
            User user = _unitOfWork.UserRepository.Get(command.Id);
            if (user == null)
                throw new Exception("The user does not exist");
            
            // Wylogowanie
            user.LogOut();
            this._unitOfWork.UserRepository.Update(user);

            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Metoda pozwalająca na zmianę hasła
        /// </summary>
        /// <param name="command"></param>
        public void Execute(ChangePasswordCommand command)
        {
            //Wyszukanie uźytkownika
            User user = _unitOfWork.UserRepository.Get(command.Id);
            if (user == null)
                throw new Exception($"User with Id '{command.Id}' already exists!");
            if (user.status != UserStatus.LoggedIn)
                throw new Exception("Please log in to change the password");

            //Sprawdzenie czy oba hasła spełniają wymagania oraz czy się zgadzają
            UserHelper.PasswordValidation(command.Password, command.RepeatPassword);

            //Sprawdzenie czy nowe hasło nie jest takie samo jak poprzednie
            if (user.Password == command.Password)
                throw new Exception("New password is the same as the old one");

            //Aktualizacja hasła
            user.UpdatePassword(command.Password);
            this._unitOfWork.UserRepository.Update(user);
            
            this._unitOfWork.Commit();
        }

        /// <summary>
        /// Usunięcie użytkownika
        /// </summary>
        /// <param name="command"></param>
        public void Execute(DeleteUserCommand command)
        {
            //Sprawdzenie czy użytkownik istnieje
            User user = _unitOfWork.UserRepository.Get(command.Id);
            if (user == null)
                throw new Exception($"User with Id '{command.Id}' already exists!");
            if (user.status != UserStatus.LoggedIn)
                throw new Exception("Please log in to delete the User");

            //Usunięcie użytkownika
            this._unitOfWork.UserRepository.Delete(user);

            this._unitOfWork.Commit();
        }
    }
}
