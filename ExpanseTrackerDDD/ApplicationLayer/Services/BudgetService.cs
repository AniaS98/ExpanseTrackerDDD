using ExpanseTrackerDDD.DomainModelLayer.Events;
using ExpanseTrackerDDD.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using ExpanseTrackerDDD.ApplicationLayer.Interfaces;
using ExpanseTrackerDDD.ApplicationLayer.Mappers;
using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Services
{
    public class BudgetService : IBudgetService
    {
        private IExpanseTrackerUnitOfWork _unitOfWork;
        private BudgetMapper _budgetMapper;
        private IDomainEventPublisher _domainEventPublisher;

        public BudgetService(IExpanseTrackerUnitOfWork unitOfWork, BudgetMapper budgetMapper)
        {
            this._unitOfWork = unitOfWork;
            this._budgetMapper = budgetMapper;
        }
        #region Methods


        #endregion

    }
}
