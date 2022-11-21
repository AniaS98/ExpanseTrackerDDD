using BaseDDD.DomainModelLayer.Models;
using ReportCreator.ApplicationLayer.Commands;
using ReportCreator.DomainModelLayer.Interfaces;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Services
{
    public class ReportService
    {
        private IAccountRepository _accountRepository;
        private ITransactionRepository _transactionRepository;
        private IBudgetRepository _budgetRepository;
        private IUserRepository _userRepository;

        public ReportService(IAccountRepository accountRepository, ITransactionRepository transactionRepository, IBudgetRepository budgetRepository, IUserRepository userRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
            _budgetRepository = budgetRepository;
            _userRepository = userRepository;
        }


        /// <summary>
        /// This method will be called once the new user is created and create they first account. It will call another methods to create empty reports
        /// </summary>
        public void CreateAllReports(Guid ownerId, ReportType reportType, ReportPeriod period)
        {
            List<Report> reports = new List<Report>();
            Report r = new Report();
            DateTime start = DateTime.Now.AddDays(-1);
            DateTime end = DateTime.Now;
            DateTime temp = DateTime.Now;

            List<Account> accounts = _accountRepository.GetAllAccountsByOwnerId(ownerId);

            switch ((int)reportType)
            {
                case 0:
                    {
                        switch((int)period)
                        {
                            case 0:
                                break;
                            case 1:
                                {
                                    start = DateTime.Now.AddDays(-2);
                                    end = DateTime.Now.AddDays(-1);
                                    break;
                                }
                            case 2:
                                {
                                    start = DateTime.Now.AddDays(-3);
                                    end = DateTime.Now.AddDays(-2);
                                    break;
                                }
                            case 3:
                                {
                                    start = DateTime.Now.AddDays(-4);
                                    end = DateTime.Now.AddDays(-3);
                                    break;
                                }
                        }
                        break;
                    }
                case 1:
                    {
                        switch ((int)period)
                        {
                            case 0:
                                {
                                    start = DateTime.Now.AddDays(-7);
                                    break;
                                }
                            case 1:
                                {
                                    start = DateTime.Now.AddDays(-14);
                                    end = DateTime.Now.AddDays(-7);
                                    break;
                                }
                            case 2:
                                {
                                    start = DateTime.Now.AddDays(-21);
                                    end = DateTime.Now.AddDays(-14);
                                    break;
                                }
                            case 3:
                                {
                                    start = DateTime.Now.AddDays(-28);
                                    end = DateTime.Now.AddDays(-21);
                                    break;
                                }
                        }
                        break;
                    }
                case 2:
                    {
                        switch ((int)period)
                        {
                            case 0:
                                {
                                    start = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                                    break;
                                }
                            case 1:
                                {
                                    temp = DateTime.Now.AddMonths(-1);
                                    start = new DateTime(temp.Year, temp.Month, 1);
                                    end = start.AddMonths(1).AddDays(-1);
                                    break;
                                }
                            case 2:
                                {
                                    temp = DateTime.Now.AddMonths(-2);
                                    start = new DateTime(temp.Year, temp.Month, 1);
                                    end = start.AddMonths(1).AddDays(-1);
                                    break;
                                }
                            case 3:
                                {
                                    temp = DateTime.Now.AddMonths(-3);
                                    start = new DateTime(temp.Year, temp.Month, 1);
                                    end = start.AddMonths(1).AddDays(-1);
                                    break;
                                }
                        }
                        break;
                    }
                case 3:
                    {
                        switch ((int)period)
                        {
                            case 0:
                                {
                                    start = new DateTime(DateTime.Now.Year, 1, 1);
                                    break;
                                }
                            case 1:
                                {
                                    start = new DateTime(DateTime.Now.Year - 1, 1, 1);
                                    end = start.AddYears(1).AddDays(-1);
                                    break;
                                }
                            case 2:
                                {
                                    start = new DateTime(DateTime.Now.Year - 2, 1, 1);
                                    end = start.AddYears(1).AddDays(-1);
                                    break;
                                }
                            case 3:
                                {
                                    start = new DateTime(DateTime.Now.Year - 3, 1, 1);
                                    end = start.AddYears(1).AddDays(-1);
                                    break;
                                }
                        }
                        break;
                    }
            }

            // Utworzenie obiektów Report niezależnych od czasu
            r = CreatePlannedPaymentReport(ownerId, accounts);
            reports.Add(r);
            Console.WriteLine(r.ToString());
            r = CreateBalancePerAccountReport(ownerId, accounts);
            reports.Add(r);
            Console.WriteLine(r.ToString());
            r = CreateRecurringDebtsReport(ownerId, accounts);
            reports.Add(r);
            Console.WriteLine(r.ToString());

            // Utworzenie obiektów Raport zależnych od czasu
            r = CreateCreditLimitUtilizationReport(ownerId, accounts, start, end);
            reports.Add(r);
            Console.WriteLine(r.ToString());
            r = CreateDebtToIncomeRatioReport(ownerId, accounts, start, end, reportType);
            reports.Add(r);
            Console.WriteLine(r.ToString());
            r = CreateExpansesStructureReport(ownerId, accounts, start, end);
            reports.Add(r);
            Console.WriteLine(r.ToString());
            r = CreateBudgetUtilizationReport(ownerId, accounts, start, end);
            reports.Add(r);
            Console.WriteLine(r.ToString());

            r = CreateTrendLineReport();
            reports.Add(r);
            Console.WriteLine(r.ToString());
        }


        private Report CreatePlannedPaymentReport(Guid ownerId, List<Account> accounts)
        {
            string name = "Planned Payments";
            DateTime start = DateTime.Now.AddDays(1);
            DateTime end = DateTime.Now.AddMonths(1);
            List<Transaction> allTransactions = new List<Transaction>();

            StringBuilder sb = new StringBuilder();
            sb.Append(name + ": \n");

            foreach(Account a in accounts)
            {
                List<Transaction> transactions = _transactionRepository.GetAllTransactionsByAccountId(a.Id);

                allTransactions.AddRange(transactions);
            }

            allTransactions.Sort(delegate (Transaction x, Transaction y) {
                return x.Date.CompareTo(y.Date);
            });

            foreach (Transaction t in allTransactions)
            {
                sb.Append(t.ToString() + ":\n");
            }

            return new Report(name, start, end, ReportType.Monthly, sb.ToString(), ownerId);
        }

        private Report CreateCreditLimitUtilizationReport(Guid ownerId, List<Account> accounts, DateTime start, DateTime end)
        {
            string name = "Credit Limit Utilization";
            StringBuilder sb = new StringBuilder();
            sb.Append(name + ": \n");
            sb.Append("Report from: " + start.ToString("dd/MM/yyyy") + " - " + end.ToString("dd/MM/yyyy") + "\n");

            foreach (Account a in accounts)
            {
                if(a.Overdraft.Amount != 0.00m)
                    sb.Append("Utilization for account " + a.Name + " is: " + a.CaluculateUtilization().ToString());
            }

            return new Report(name, start, end, ReportType.Monthly, sb.ToString(), ownerId);
        }

        private Report CreateBalancePerAccountReport(Guid ownerId, List<Account> accounts)
        {
            string name = "Balance Per Account";
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;

            StringBuilder sb = new StringBuilder();
            sb.Append(name + ": \n");

            foreach (Account a in accounts)
            {
                sb.Append("Account " + a.Name + " balance: " + a.AccountBalance + " available funds: " + (a.AccountBalance+a.Overdraft).ToString() + "\n");
            }

            return new Report(name, start, end, ReportType.Daily, sb.ToString(), ownerId);
        }

        private Report CreateDebtToIncomeRatioReport(Guid ownerId, List<Account> accounts, DateTime start, DateTime end, ReportType reportType)
        {
            string name = "Debt to Income Ratio";
            StringBuilder sb = new StringBuilder();
            sb.Append(name + ":\n");

            //rozpatrujemy wartość w głównej walucie
            Money Debt = new Money(0, CurrencyName.PLN);
            Money Income = new Money(0, CurrencyName.PLN);

            foreach (Account a in accounts)
            {
                List<Transaction> transactions = _transactionRepository.GetAllTransactionsByAccountId(a.Id);

                if (a.Currency == CurrencyName.PLN)
                {
                    foreach (var t in transactions)
                    {
                        if(t.Date >= start && t.Date <= end)
                        {
                            if (t.Type == "Income")
                                Income += t.Value;
                            else if (t.Type == "Expanse")
                                Debt += t.Value;
                        }                        
                    }
                }
                else
                {
                    //zronić zamianę waluty
                    foreach (var t in transactions)
                    {
                        if (t.Date >= start && t.Date <= end)
                        {
                            if (t.Type == "Income")
                                Income += t.Value;
                            else if (t.Type == "Expanse")
                                Debt += t.Value;
                        }
                    }
                }
            }

            decimal Ratio = Debt.Amount / Income.Amount;

            sb.Append("Debts: " + Debt.ToString() + "\n");
            sb.Append("Income: " + Income.ToString() + "\n");
            sb.Append("Ratio: " + Ratio.ToString());

            return new Report(name, start, end, reportType, sb.ToString(), ownerId);
        }

        private Report CreateRecurringDebtsReport(Guid ownerId, List<Account> accounts)
        {
            string name = "Recurring Debts";
            DateTime start = DateTime.Now.AddDays(1);
            DateTime end = DateTime.Now.AddMonths(1).AddDays(1);
            StringBuilder sb = new StringBuilder();
            sb.Append(name + ":\n");

            foreach (Account a in accounts)
            {
                List<Transaction> transactions = _transactionRepository.GetAllTransactionsByAccountId(a.Id);
                foreach (var t in transactions)
                {
                    if (t.Date >= start && t.Date <= end)
                        if (t.Frequency == "Reoccuring")
                            sb.Append(t.ToString() + "\n");
                }
            }

            return new Report(name, start, end, ReportType.Monthly, sb.ToString(), ownerId);
        }

        private Report CreateExpansesStructureReport(Guid ownerId, List<Account> accounts, DateTime start, DateTime end)
        {
            string name = "Expanses Structure";
            StringBuilder sb = new StringBuilder();
            sb.Append(name + ":\n");
            sb.Append(start.ToString("dd/MM/yyyy") + " - " + end.ToString("dd/MM/yyyy") + " expanse structure:\n");
            Money AllExpanses = new Money(0, CurrencyName.PLN);
            Dictionary<string, Money> ExpansesByCategories = new Dictionary<string, Money>();

            foreach (Account a in accounts)
            {
                List<Transaction> transactions = _transactionRepository.GetAllTransactionsByAccountId(a.Id);

                if (a.Currency == CurrencyName.PLN)
                {
                    foreach (var t in transactions)
                    {
                        if (t.Date >= start && t.Date <= end)
                        {
                            if(t.Type == "Expanse" || t.Type == "Borrowed")
                            {
                                AllExpanses += t.Value;
                                if (ExpansesByCategories.ContainsKey(t.Category.ToString()))
                                {
                                    ExpansesByCategories[t.Category.ToString()] += t.Value;
                                }
                                else
                                {
                                    ExpansesByCategories.Add(t.Category.ToString(), t.Value);
                                }
                            }
                        }
                    }
                }
                else
                {//zamienic walutę
                    foreach (var t in transactions)
                    {
                        if (t.Date >= start && t.Date <= end)
                        {
                            if (t.Type == "Expanse" || t.Type == "Borrowed")
                            {
                                AllExpanses += t.Value;
                                if (ExpansesByCategories.ContainsKey(t.Category.ToString()))
                                {
                                    ExpansesByCategories[t.Category.ToString()] += t.Value;
                                }
                                else
                                {
                                    ExpansesByCategories.Add(t.Category.ToString(), t.Value);
                                }
                            }
                        }
                    }

                }
                    
            }

            foreach(KeyValuePair<string, Money> pair in ExpansesByCategories)
            {
                sb.Append(pair.Key + ": " + Math.Round(pair.Value.Amount / AllExpanses.Amount, 2).ToString() + "%\n");
            }

            return new Report(name, start, end, ReportType.Monthly, sb.ToString(), ownerId);
        }

        private Report CreateTrendLineReport()
        {
            throw new Exception();
            //return new Report(name, start, end, ReportType.Monthly, sb.ToString(), ownerId);
        }

        private Report CreateBudgetUtilizationReport(Guid ownerId, List<Account> accounts, DateTime start, DateTime end)
        {
            string name = "Budget Utilization";
            StringBuilder sb = new StringBuilder();
            sb.Append(name + ":\n");
            sb.Append(start.ToString("dd/MM/yyyy") + " - " + end.ToString("dd/MM/yyyy") + " budget utilization:\n");
            Dictionary<Guid, (Money,Money)> BudgetsUtilization = new Dictionary<Guid, (Money, Money)>();

            foreach (Account a in accounts)
            {
                List<Transaction> transactions = _transactionRepository.GetAllTransactionsByAccountId(a.Id);
                foreach (var t in transactions)
                {
                    if (t.Date >= start && t.Date <= end)
                    {
                        if (BudgetsUtilization.ContainsKey(t.BudgetId))
                        {
                            //BudgetsUtilization.
                              //  += t.Value;
                        }
                        else
                        {
                            BudgetsUtilization.Add(t.BudgetId, (t.Value, _budgetRepository.GetBudgetById(t.BudgetId).Limit));
                        }
                    }
                }
            }

            foreach (KeyValuePair<Guid, (Money, Money)> pair in BudgetsUtilization)
            {
                sb.Append(pair.Key + ": " + (pair.Value.Item1/ pair.Value.Item2).ToString() + "%\n");
            }

            return new Report(name, start, end, ReportType.Monthly, sb.ToString(), ownerId);
        }


    }
}
