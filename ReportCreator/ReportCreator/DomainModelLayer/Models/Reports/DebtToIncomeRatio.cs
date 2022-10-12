using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models.Reports
{
    public class DebtToIncomeRatio : Report
    {
        public Money Debt { get; protected set; }
        public Money Income { get; protected set; }
        public decimal Ratio { get; protected set; }
        public string DefaultCurrency { get; protected set; }

        //Factory do obliczania wpływów/odpływów z transakcji
        public DebtToIncomeRatio(string name, DateTime startDate, DateTime endDate, Guid ownerId, List<Transaction> listOfTransactions, string defaultCurrency, ReportType reportType = ReportType.Monthly) : base(startDate, endDate, ownerId)
        {
            DefaultCurrency = defaultCurrency;

            //dodać obsługę innych walut
            Debt = new Money(0, DefaultCurrency);
            Income = new Money(0, DefaultCurrency);

            foreach (var transaction in listOfTransactions)
            {
                if (transaction.Type == "Income")
                    Income += transaction.Value;
                else if (transaction.Type == "Expanse")
                    Debt += transaction.Value;
            }

            Ratio = Debt.Amount / Income.Amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name + "\n");
            sb.Append("Debts: " + Debt.ToString() +"\n");
            sb.Append("Income: " + Income.ToString() + "\n");
            sb.Append("Ratio: " + Ratio.ToString() );

            return sb.ToString();
        }






    }
}
