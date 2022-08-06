using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Factories
{
    public class TransactionFactory
    {
        private IDomainEventPublisher _domainEventPublisher;

        public TransactionFactory(IDomainEventPublisher domainEventPublisher)
        {
            this._domainEventPublisher = domainEventPublisher;
        }
        public Transaction Create(TransactionDto transactionDto)
        {
            Currency currency = new Currency((CurrencyName)transactionDto.Value.Currency.Base, (CurrencyName)transactionDto.Value.Currency.Name);
            Money value = new Money(transactionDto.Value.Amount, currency);

            return new Transaction(new Guid(), _domainEventPublisher, transactionDto.Description, (TransactionType)transactionDto.Type, value, new Category(), new Subcategory(), transactionDto.TransactionDate, (TransactionStatus)transactionDto.Status, transactionDto.AccountId, transactionDto.Contractor, transactionDto.Note);
        }

        public void CreateTransfer(TransactionDto transactionDto, Guid destinationAccountId)
        {
            Transaction from = Create(transactionDto);
            transactionDto.Id = new Guid();
            transactionDto.Type = TransactionTypeDto.Income;
            transactionDto.AccountId = destinationAccountId;
            Transaction to = Create(transactionDto);
        }




    }
}
