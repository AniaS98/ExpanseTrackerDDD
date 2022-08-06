using ExpanseTrackerDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.ApplicationLayer.Interfaces;
using ExpanseTrackerDDD.DomainModelLayer.Factories;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Services
{
    public class TransactionService : ITransactionService
    {
        private IExpanseTrackerUnitOfWork _unitOfWork;
        private TransactionFactory _transactionFactory;

        public TransactionService(IExpanseTrackerUnitOfWork unitOfWork, TransactionFactory transactionFactory)
        {
            this._unitOfWork = unitOfWork;
            this._transactionFactory = transactionFactory;
        }
        #region Methods

        public void CreateTransaction(TransactionDto transactionDto)
        {
            CheckIfAccountExist(transactionDto.AccountId);

            Transaction transaction = _transactionFactory.Create(transactionDto);
        }

        public void UpdateTransaction(TransactionDto transactionDto)
        {
            Transaction transaction = _unitOfWork.TransactionRepository.Find(x => x.Id == transactionDto.Id).FirstOrDefault();

            if (transaction == null)
                throw new Exception($"Could not find transaction '{transactionDto.Id}'");

            Currency currency = new Currency((CurrencyName)transactionDto.Value.Currency.Base, (CurrencyName)transactionDto.Value.Currency.Name);
            Money value = new Money(transactionDto.Value.Amount, currency);

            transaction.UpdateTransaction(transactionDto.Description, (TransactionType)transactionDto.Type, value, new Category(), new Subcategory(), transactionDto.TransactionDate, (TransactionStatus)transactionDto.Status, transactionDto.AccountId, transactionDto.Contractor, transactionDto.Note);

        }

        public void CreateTransfer(TransactionDto transactionDto, Guid destinationAccountId)
        {
            Account oldAccount = CheckIfAccountExist(transactionDto.AccountId);
            Account newAccount = CheckIfAccountExist(destinationAccountId);
            List<Transaction> transactions = new List<Transaction>();

            if (oldAccount.AccountCurrency != newAccount.AccountCurrency)
                _transactionFactory.CreateTransfer(transactionDto, destinationAccountId);

        }






        public Account CheckIfAccountExist(Guid IdToFind)
        {
            Account account = this._unitOfWork.AccountRepository.Find(x => x.Id.Equals(IdToFind)).FirstOrDefault();
            if (account == null)
            {
                throw new Exception("Account does not exist");
            }
            return account;
        }



        #endregion


    }
}
