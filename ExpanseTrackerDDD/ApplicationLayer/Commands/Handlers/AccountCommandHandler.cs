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
        private IDomainEventPublisher _domainEventPublisher;

        public AccountCommandHandler(IExpanseTrackerUnitOfWork unitOfWork, IDomainEventPublisher domainEventPublisher)
        {
            _unitOfWork = unitOfWork;

            _domainEventPublisher = domainEventPublisher;
        }

        public void Execute(CreateAccountCommand command)
        {
            User user = this._unitOfWork.UserRepository.Get(command.UserId);
            if (user == null)
                throw new Exception($"User with Id '{command.UserId}' does not exist!");

            Account account = this._unitOfWork.AccountRepository.Get(command.Id);
            if (account != null)
                throw new Exception($"Account with Id '{command.Id}' already exists!");

            account = this._unitOfWork.AccountRepository.GetAccountByName(command.Name);
            if (account != null)
                throw new Exception($"Account with name '{command.Name}' already exists!");

            account = this._unitOfWork.AccountRepository.GetAccountByNumber(command.AccountNumber);
            if (account != null)
                throw new Exception($"Account with number '{command.AccountNumber}' already exists!");


            account = new Account(new Guid(), _domainEventPublisher, command.Name, command.AccountNumber, command.Type, command.CurrencyName, command.UserId);

            this._unitOfWork.AccountRepository.Insert(account);
            this._unitOfWork.Commit();
        }

        public void Execute(UpdateAccount command)
        {
            Account account = this._unitOfWork.AccountRepository.Get(command.Id);
            if (account == null)
                throw new Exception($"Account with Id '{command.Id}' does not exist!");

            if (command.Name != account.Name)
                account.UpdateName(command.Name);
            if (command.Color != account.Color)
                account.UpdateColor(command.Color);
            if (command.AccountNumber != account.AccountNumber)
                account.UpdateAccountNumber(command.AccountNumber);
            if (command.Type != account.Type)
                account.UpdateType(command.Type);

            this._unitOfWork.AccountRepository.Update(account);
            this._unitOfWork.Commit();

        }









    }
}
