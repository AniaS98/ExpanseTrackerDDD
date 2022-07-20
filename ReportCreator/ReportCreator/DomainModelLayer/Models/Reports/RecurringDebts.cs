using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models.Reports
{
    public class RecurringDebts : Report
    {
        public Dictionary<string, Money> Debts { get; protected set; }

        public RecurringDebts(string name, DateTime startDate, DateTime endDate) : base(name, startDate, endDate)
        {
            Debts = new Dictionary<string, Money>();
            foreach(var account in Accounts)
            {
            }


        }

    }
}
