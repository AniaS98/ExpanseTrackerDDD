using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Mappers
{
    public class TransactionMapper
    {
        public List<TransactionDto> Map(IList<Transaction> transactions)
        {
            List<TransactionDto> result = new List<TransactionDto>();
            foreach(Transaction transaction in transactions)
            {
                TransactionDto transactionDto = Map(transaction);
                result.Add(transactionDto);
            }
            return result;
        }

        public TransactionDto Map(Transaction transaction)
        {
            return new TransactionDto()
            {
                Id = transaction.Id,
                Contractor = transaction.Contractor,
                Description = transaction.Description,
                Frequency = (TransactionFrequencyDto)transaction.Frequency,
                Note = transaction.Note,
                Status = (TransactionStatusDto)transaction.Status,
                TransactionCategory = Mappers.Map(transaction.TransactionCategory),
                TransactionDate = transaction.TransactionDate,
                TransactionRecurrency = Mappers.Map(transaction.TransactionRecurrency),
                Type = (TransactionTypeDto)transaction.Type,
                Value = Mappers.Map(transaction.Value),
                AccountId = transaction.AccountId
            };
        }

    }
}
