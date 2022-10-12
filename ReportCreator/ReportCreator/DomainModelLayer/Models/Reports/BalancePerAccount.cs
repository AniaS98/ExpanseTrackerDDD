using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models.Reports
{
    public class BalancePerAccount : Report
    {
        public Dictionary<(Guid, string),Money> AccountsBalances { get; protected set; }

        public BalancePerAccount(DateTime startDate, DateTime endDate, Guid ownerId, Dictionary<(Guid, string), Money> accountsBalances, ReportType reportType=ReportType.Daily) : base(startDate, endDate, ownerId)
        {
            Name = "Balances of All users account";
            AccountsBalances = accountsBalances;            
        }

        public void UpdateBalance(Account account, Money balance)
        {
            AccountsBalances[(account.AccountId, account.Name)] = balance;
        }

        public override string ToString()
        {

            StringBuilder sb = new StringBuilder();
            sb.Append(Name +"\n");
            sb.Append("Report from: " + StartDate.ToString("dd/MM/yyyy") + " - " + EndDate.ToString("dd/MM/yyyy") + "\n");

            foreach (var a in AccountsBalances)
            {
                sb.Append("Account " + a.Key.Item2 + " balance: " + a.Value + "\n");
            }
            
            return sb.ToString();
        }


    }
}
