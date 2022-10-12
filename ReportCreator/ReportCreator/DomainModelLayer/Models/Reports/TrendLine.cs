using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.DomainModelLayer.Models.Reports
{
    public class TrendLine : Report
    {
        //public Dictionary<>
        
        public TrendLine(DateTime startDate, DateTime endDate, Guid ownerId) : base(startDate, endDate, ownerId)
        {
            Name = "Expanses by Categories";

        }

        public void CalculateTrend()
        {

        }


        public override string ToString()
        {



            return base.ToString();
        }




    }
}
