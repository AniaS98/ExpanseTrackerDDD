using Base.DomainModelLayer.Interfaces;
using ExpanseTrackerDDD.ApplicationLayer.Mappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Services
{
    public class BudgetService
    {
        private IUnitOfWork _unitOfWork;
        //private AccountFactory _accountFactory;
        private BudgetMapper _budgetMapper;

        public BudgetService(IUnitOfWork unitOfWork, BudgetMapper budgetMapper)
        {
            this._unitOfWork = unitOfWork;
            this._budgetMapper = budgetMapper;
        }
        #region Methods

        #endregion

    }
}
