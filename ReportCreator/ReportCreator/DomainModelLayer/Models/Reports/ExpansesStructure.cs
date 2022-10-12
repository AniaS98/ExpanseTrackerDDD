using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models.Reports
{
    public class ExpansesStructure : Report
    {
        public string DefaultCurrency { get; protected set; }
        public Dictionary<string, decimal> PercentageOfExpanses { get; protected set; }
        public ExpansesStructure(string name, DateTime startDate, DateTime endDate, Guid ownerId, string defaultCurrency, List<Transaction> listOfTransactions, ReportType reportType = ReportType.Monthly) : base(startDate, endDate, ownerId)
        {
            Dictionary<string, Money> ExpansesByCategories = new Dictionary<string, Money>();
            PercentageOfExpanses = new Dictionary<string, decimal>();

            DefaultCurrency = defaultCurrency;
            Money AllExpanses = new Money(0, DefaultCurrency);
            foreach(var transaction in listOfTransactions)
            {
                AllExpanses += transaction.Value;
                if (transaction.Type != "Income" && transaction.Type != "Transfer" && transaction.Type != "Exchange")
                {
                    if(ExpansesByCategories.ContainsKey(transaction.Category.ToString()))
                    {
                        ExpansesByCategories[transaction.Category.ToString()] += transaction.Value;
                    }
                    else
                    {
                        ExpansesByCategories.Add(transaction.Category.ToString(), transaction.Value);
                    }
                }
            }

            foreach(var element in ExpansesByCategories)
            {
                PercentageOfExpanses.Add(element.Key, (element.Value / AllExpanses).Amount * 100.0m);
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name + "\n");
            sb.Append(StartDate.ToString("dd/MM/yyyy") + " - " + EndDate.ToString("dd/MM/yyyy") + " expanse structure\n");
            foreach (var element in PercentageOfExpanses)
            {
                sb.Append(element.Key + ":\t" + element.Value + "%");
            }


            return sb.ToString();
        }


    }
}
