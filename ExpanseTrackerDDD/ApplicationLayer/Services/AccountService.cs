using ExpanseTrackerDDD.ApplicationLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Factories;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Services
{
    public class AccountService : IAccountService
    {
        private IExpanseTrackerUnitOfWork _unitOfWork;
        private AccountFactory _accountFactory;

        public AccountService(IExpanseTrackerUnitOfWork unitOfWork, AccountFactory accountFactory)
        {
            this._unitOfWork = unitOfWork;
            this._accountFactory = accountFactory;
        }

        #region Methods
        public List<Account> GetAllAccounts()
        {
            return (List<Account>)this._unitOfWork.AccountRepository.GetAll();
        }
        public Account GetAccount(Guid IdToFind)
        {
            return ((List<Account>)this._unitOfWork.AccountRepository.Find(x => x.Id.Equals(IdToFind)))[0];
        }





        #endregion
    }
}
