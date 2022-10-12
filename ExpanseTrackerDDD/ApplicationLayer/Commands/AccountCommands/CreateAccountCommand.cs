using ExpanseTrackerDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.AccountCommands
{
    public class CreateAccountCommand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public AccountType Type { get; set; }
        public CurrencyName CurrencyName { get; set; }
        public string Color { get; set; }
        public Guid UserId { get; set; }
    }
}
