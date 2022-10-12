using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.BudgetCommands
{
    public class RenewBudgetCommand
    {
        public Guid userId { get; set; }
    }
}
