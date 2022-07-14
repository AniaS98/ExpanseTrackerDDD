using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models.Reports
{
    public class PlannedPayments : Report
    {
        public PlannedPayments(string name, DateTime startDate, DateTime endDate) : base(name, startDate, endDate)
        {

        }


    }
}
