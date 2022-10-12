using ReportCreator.DomainModelLayer.Interfaces;
using ReportCreator.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.Commands.Handlers
{
    public class CommandHandler
    {
        private IReportCreatorUnitOfWork _unitOfWork;
        // factories
        
        public CommandHandler(IReportCreatorUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




    }
}
