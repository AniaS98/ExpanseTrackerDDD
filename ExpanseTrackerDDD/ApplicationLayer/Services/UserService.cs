using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.ApplicationLayer.Interfaces;
using ExpanseTrackerDDD.ApplicationLayer.Mappers;
using ExpanseTrackerDDD.DomainModelLayer.Factories;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Services
{
    public class UserService : IUserService
    {
        private IExpanseTrackerUnitOfWork _unitOfWork;
        private UserFactory _userFactory;
        private UserMapper _userMapper;
        private IDomainEventPublisher _domainEventPublisher;

        public UserService(IExpanseTrackerUnitOfWork unitOfWork, UserMapper userMapper)
        {
            this._unitOfWork = unitOfWork;
            this._userMapper = userMapper;
        }
        #region Methods

        public void CreateUser(UserDto userDto)
        {
            if (_unitOfWork.UserRepository.Find(x => x.Id == userDto.Id) != null)
                throw new Exception("User already exist");

            if (_unitOfWork.UserRepository.Find(x => x.Login == userDto.Login) != null)
                throw new Exception("User with this login already exists!");

            User user = _userFactory.Create(userDto);
            this._unitOfWork.UserRepository.Insert(user);
            this._unitOfWork.Commit();
        }

        public User Login(string login, string password)
        {
            User user = _unitOfWork.UserRepository.Find(x => x.Login == login)[0];
            if (user == null)
                throw new Exception("Incorrect login");

            if (user.Password != password)
                throw new Exception("Incorrect password");

            return user;
        }

        public void ChangePassword(Guid id, string password, string repeatPassword)
        {
            VerifyPasswords(password, repeatPassword);

            User user = _unitOfWork.UserRepository.Find(x => x.Id == id)[0];

            if (user.Password == password)
                throw new Exception("New password is the same as the old one");

            user.UpdatePassword(password);
        }

        public void VerifyPasswords(string password, string repeatPassword)
        {
            if (password != repeatPassword)
                throw new Exception("Passwords do not match. Please try again");
        }





        #endregion
    }
}
