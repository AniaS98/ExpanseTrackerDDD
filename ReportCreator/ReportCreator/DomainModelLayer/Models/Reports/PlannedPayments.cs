using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models.Reports
{
    public class PlannedPayments : Report
    {
        private List<Transaction> transactions = new List<Transaction>();
        public ReadOnlyCollection<Transaction> Transactions { get { return this.transactions.AsReadOnly(); } }

        //factory
        public PlannedPayments(string name, DateTime startDate, DateTime endDate, Guid ownerId, List<Transaction> listOfTransactions, ReportType reportType = ReportType.Monthly) : base(startDate, endDate, ownerId)
        {
            foreach(var transaction in listOfTransactions)
            {
                if (transaction.Status == "Planned")
                    transactions.Add(transaction);
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name + "\n");
            sb.Append("Upcomming payments:\n");
            foreach(var transaction in transactions)
            {
                sb.Append(transaction.ToString() + "\n");
            }

            return sb.ToString();
        }


    }
}
