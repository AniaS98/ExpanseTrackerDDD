using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models.Reports
{
    public class BalancePerAccount : Report
    {
        public Dictionary<string, Money> AccountBalances { get; protected set; }
        public BalancePerAccount(string name, DateTime startDate, DateTime endDate, Dictionary<string, Money> accountBalances) : base(name, startDate, endDate)
        {
            this.AccountBalances = accountBalances;
        }

    }
}
