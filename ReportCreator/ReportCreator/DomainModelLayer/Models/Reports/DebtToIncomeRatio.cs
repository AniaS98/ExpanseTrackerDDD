using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models.Reports
{
    public class DebtToIncomeRatio : Report
    {
        public Money Debt { get; protected set; }
        public Money Income { get; protected set; }

        public DebtToIncomeRatio(string name, DateTime startDate, DateTime endDate) : base(name, startDate, endDate)
        {
            this.Debt = new Money(0, "PLN");//zmienić logikę na pokazanie tego raportu w walucie domyślnej
            foreach(var account in Accounts)
            {
                /*
                foreach(var transaction in account.Transactions)
                {
                    if (transaction.Type == "Income")
                        Income += transaction.Value;
                    else if(transaction.Type == "Expanse")
                        Debt += transaction.Value;
                }*/
            }
        
        }



    }
}
