using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models.Reports
{
    public class CreditLimitsUtilization : Report
    {
        public Money Limit { get; protected set; }
        //ewentualnie dopisać

        public CreditLimitsUtilization(string name, DateTime startDate, DateTime endDate, Money limit) : base(name, startDate, endDate)
        {
            this.Limit = limit;
        }

    }
}
