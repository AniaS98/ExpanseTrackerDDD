using BaseDDD.DomainModelLayer.Events;
using ReportCreator.DomainModelLayer.Interfaces;
using ReportCreator.DomainModelLayer.Models;
using ReportCreator.DomainModelLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportCreator.ApplicationLayer.Commands.Handlers
{
    public class ReportCommandHandler
    {
        private IReportCreatorUnitOfWork _unitOfWork;
        private ReportService _reportService;

        public ReportCommandHandler(IReportCreatorUnitOfWork unitOfWork, ReportService reportService)
        {
            _unitOfWork = unitOfWork;
            _reportService = reportService;
        }

        public void Execute(ShowAllReportsCommand command)
        {
            // Dodawanie wszelki zmian w klasach modelu
            _unitOfWork.CheckEventsAndUpdate(command.eventBus);

            // Wyszukanie użytkownika
            User user = _unitOfWork.UserRepository.GetUserById(command.UserId);

            // Sprawdzenie czy użytkownik isnieje
            if (user == null)
                throw new Exception("Report could not be shown, since the user does not exist");
            // Sprawdzenie czy użytkownik jest zalogowany
            if (user.Status == "LoggedOut")
                throw new Exception("Report could not be shown, please log in");

            //Tworzenie raportów
            _reportService.CreateAllReports(command.UserId, command.ReportType, command.ReportPeriod);


        }



    }
}
