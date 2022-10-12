using ReportCreator.DomainModelLayer.Interfaces;
using ReportCreator.DomainModelLayer.Models;
using ReportCreator.DomainModelLayer.Models.Reports;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Services
{
    public class CreateReports
    {
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;

        public CreateReports(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }
        /// <summary>
        /// This method will be called once the new user is created and create they first account. It will call another methods to create empty reports
        /// </summary>
        public void CreateAllReports(Guid ownerId)
        {
            //Get the account of said user
            List<Account> accounts = _accountRepository.GetAllAccountsByOwnerId(ownerId);

            DateTime temp = DateTime.Now;

            //Daily
            //Start date
            DateTime startDate = new DateTime(temp.Year, temp.Month, temp.Day, 0, 0, 0);
            //End date
            DateTime endDate = new DateTime(temp.Year, temp.Month, temp.Day, 23, 59, 59);

            CreateBalancePerAccount(ownerId, accounts, startDate, endDate);


            //Weekly
            //Start date
            if (startDate.DayOfWeek > DayOfWeek.Monday)
                startDate.AddDays(-(int)startDate.DayOfWeek + 1);
            else if (startDate.DayOfWeek == DayOfWeek.Monday)
                startDate.AddDays(-6);
            //End date
            temp = startDate.AddDays(7);
            endDate = new DateTime(temp.Year, temp.Month, temp.Day, 23, 59, 59);



            //Monthly
            //Start date
            startDate = new DateTime(temp.Year, temp.Month, 1, 0, 0, 0);
            //End date
            temp = startDate.AddMonths(1).AddDays(-1);
            endDate = new DateTime(temp.Year, temp.Month, temp.Day, 23, 59, 59);



            //Annual
            //Start date
            startDate = new DateTime(temp.Year, 1, 1, 0, 0, 0);
            //End date
            temp = startDate.AddYears(1).AddDays(-1);
            endDate = new DateTime(temp.Year, temp.Month, temp.Day, 23, 59, 59);

        }

        public void CreateBalancePerAccount(Guid ownerId, List<Account> accounts, DateTime startDate, DateTime endDate)
        {
            Dictionary<(Guid, string), Money> accountsBalances = new Dictionary<(Guid, string), Money>();
            accountsBalances.Add((accounts[0].AccountId, accounts[0].Name), accounts[0].AccountBalance);

            BalancePerAccount bpa = new BalancePerAccount(startDate, endDate, ownerId, accountsBalances);
            bpa.ToString();
            Console.WriteLine("------------------------------------------\n");
        }



    }
}
