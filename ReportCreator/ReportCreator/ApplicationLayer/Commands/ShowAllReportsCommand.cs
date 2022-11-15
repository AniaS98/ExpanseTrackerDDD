using BaseDDD.DomainModelLayer.Models;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.Commands
{
    public enum ReportPeriod
    {
        Current,
        PreviousPeriod,
        TwoPeriodsBefore,
        ThreePeriodsBefore
    }

    public class ShowAllReportsCommand
    {
        public EventBus eventBus { get; set; }
        public Guid UserId { get; set; }
        public ReportType ReportType { get; set; }
        public ReportPeriod ReportPeriod { get; set; }
    }
}
