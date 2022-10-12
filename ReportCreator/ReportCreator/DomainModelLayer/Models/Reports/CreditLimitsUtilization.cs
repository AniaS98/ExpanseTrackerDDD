using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models.Reports
{
    public class CreditLimitsUtilization : Report
    {
        public Account Account { get; protected set; }
        public Money Limit { get; protected set; }
        //ewentualnie dopisać

        public CreditLimitsUtilization(string name, DateTime startDate, DateTime endDate, Guid ownerId, Account account, Money limit, ReportType reportType = ReportType.Monthly) : base(startDate, endDate, ownerId)
        {
            this.Account = account;
            this.Limit = limit;
        }

        public decimal CaluculateUtilization()
        {
            if(Account.AccountBalance.Amount > 0.00m)
                return 0.00m;

            if( Account.AccountBalance.CurrencyName == Limit.CurrencyName)
                return (-Account.AccountBalance.Amount) / Limit.Amount;

            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name + "\n");
            sb.Append("Report from: " + StartDate.ToString("dd/MM/yyyy") + " - " + EndDate.ToString("dd/MM/yyyy") + "\n");
            sb.Append("Utilization for account " + Account.Name + " is: " + CaluculateUtilization().ToString());

            return sb.ToString();
        }






    }
}
