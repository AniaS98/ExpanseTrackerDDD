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
        private IDomainEventPublisher _domainEventPublisher;
        //wyczyścić konstruktor
        public UserCommandHandler(IExpanseTrackerUnitOfWork unitOfWork, IDomainEventPublisher domainEventPublisher)
        {
            _unitOfWork = unitOfWork;
            _domainEventPublisher = domainEventPublisher;
        }


        public void Execute(CreateUserCommand command)
        {
            User user = this._unitOfWork.UserRepository.Get(command.Id);
            if (user != null)
                throw new Exception($"User with Id '{command.Id}' already exists!");

            user = this._unitOfWork.UserRepository.GetUserByLogin(command.Login);
            if (user != null)
                throw new Exception($"User with login '{command.Login}' already exists!");

            //CHYBA BEZ TRY CATCHA
            UserHelper.VerifyPasswords(command.Password, command.RepeatPassword);

            user = new User(command.Id, command.Login, command.Password, command.FirstName, command.LastName);

            this._unitOfWork.UserRepository.Insert(user);
            this._unitOfWork.Commit();
        }

        public void Execute(LoginCommand command)
        {
            //Czy to tutaj pisać?
            User user = _unitOfWork.UserRepository.GetUserByLogin(command.Login);
            if (user == null)
                throw new Exception("Incorrect login");

            if (user.Password != command.Password)
                throw new Exception("Incorrect password");
        }

        public void Execute(ChangePasswordCommand command)
        {
            UserHelper.VerifyPasswords(command.Password, command.RepeatPassword);

            User user = _unitOfWork.UserRepository.Get(command.Id);

            if (user.Password == command.Password)
                throw new Exception("New password is the same as the old one");

            user.UpdatePassword(command.Password);
            this._unitOfWork.UserRepository.Update(user);
            this._unitOfWork.Commit();
        }







    }
}
