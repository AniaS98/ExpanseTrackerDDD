using BaseDDD.DomainModelLayer.Models;
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

    public class Report : ValueObject
    {
        public string Name { get; protected set; }
        public DateTime StartDate { get; protected set; }
        public DateTime EndDate { get; protected set; } 
        public ReportType ReportType { get; protected set; }
        public string Result { get; protected set; }
        public Guid OwnerId { get; protected set; }

        public Report() { }

        public Report(string name, DateTime startDate, DateTime endDate, ReportType reportType, string result, Guid ownerId)
        {
            this.Name = name;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.ReportType = reportType;
            this.Result = result;
            this.OwnerId = ownerId;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Name);

            return base.ToString();
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            throw new NotImplementedException();
        }
    }
}
