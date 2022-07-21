using ExpanseTrackerDDD.DomainModelLayer.Factories;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace ExpanseTrackerDDD.ApplicationLayer.Services
{
    public class TransactionService
    {
        private IExpanseTrackerUnitOfWork _unitOfWork;
        private TransactionFactory _transactionFactory;

        public TransactionService(IExpanseTrackerUnitOfWork unitOfWork, TransactionFactory transactionFactory)
        {
            this._unitOfWork = unitOfWork;
            this._transactionFactory = transactionFactory;
        }
        #region Methods

        public void CreateUpcomingTransaction(string description, Type   Guid AccountId)
        {
            CheckIfAccountExist(AccountId);

            Transaction transaction = new Transaction()
        }




        public void CheckIfAccountExist(Guid IdToFind)
        {
            if (this._unitOfWork.AccountRepository.Find(x => x.Id.Equals(IdToFind)).Count == 0)
            {
                throw new Exception("Account does not exist");
            }
        }



        #endregion


    }
}
