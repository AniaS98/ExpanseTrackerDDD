using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models
{
    public enum ReportType
    {
        Daily,
        Weekly,
        Monthly,
        Annual
    }

    public abstract class Report
    {
        public string Name { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; }
        public ReportType ReportType { get; protected set; }
        public Guid OwnerId { get; protected set; }

        public Report(DateTime startDate, DateTime endDate, Guid ownerId)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.OwnerId = ownerId;
        }

        public void SetDates(DateTime startDate, DateTime endDate)
        {
            this.StartDate = startDate;
            this.EndDate = endDate;
        }
        //Możliwe, że będzie gdzieś indziej
        public void Renew()
        {

        }



    }
}
