using ReportCreator.DomainModelLayer.Interfaces;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Services
{
    public class RenewReports
    {
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;

        public RenewReports(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public void RenewAllReports()
        {



        }

        public void RenewBalancePerAccount(string name, DateTime startDate, DateTime endDate, Guid ownerId, ReportType reportType)
        {
            // Verification if the report should be renewed


            List<Account> accounts = _accountRepository.GetAllAccountsByOwnerId(ownerId);


        }


    }

}
