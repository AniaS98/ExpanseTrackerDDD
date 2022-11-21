using BaseDDD.DomainModelLayer.Models;
using ReportCreator.ApplicationLayer.DTOs;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.Mappers
{
    public class TransactionMapper
    {
        public TransactionDto Map(Transaction transaction)
        {
            return  new TransactionDto()
            {
                Id = transaction.Id,
                AccountId = transaction.AccountId,
                Name = transaction.Name,
                Category = transaction.Category,
                Date = transaction.Date,
                Frequency = transaction.Frequency,
                Status = transaction.Status,
                Type = transaction.Type,
                Value = MoneyMapper.Map(transaction.Value)
            };

        }
    }
}

