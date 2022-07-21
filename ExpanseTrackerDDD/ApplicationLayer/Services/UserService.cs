using Base.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.ApplicationLayer.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Services
{
    public class UserService
    {
        private IUnitOfWork _unitOfWork;
        //private AccountFactory _accountFactory;
        private UserMapper _userMapper;

        public UserService(IUnitOfWork unitOfWork, UserMapper userMapper)
        {
            this._unitOfWork = unitOfWork;
            this._userMapper = userMapper;
        }
        #region Methods

        #endregion
    }
}
