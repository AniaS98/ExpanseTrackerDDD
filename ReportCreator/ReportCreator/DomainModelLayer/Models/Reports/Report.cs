using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models
{
    public abstract class Report
    {
        public string Name { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        //Klasa z drugiego kontekstu
        private List<Account> accounts = new List<Account>();
        public ReadOnlyCollection<Account> Accounts { get { return this.accounts.AsReadOnly(); } }

        public Report(string name, DateTime startDate, DateTime endDate)
        {
            this.Name = name;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.accounts = new List<Account>();
            MapAccounts();
        }

        public void SetDates(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }

        public void MapAccounts()
        {
            //Zapisywanie kont użytkownika
            throw new NotImplementedException();
        }



    }
}
