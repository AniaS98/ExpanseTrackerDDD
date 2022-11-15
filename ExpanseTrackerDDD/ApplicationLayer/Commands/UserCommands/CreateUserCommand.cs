using BaseDDD.DomainModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.UserCommands
{
    public class CreateUserCommand
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
