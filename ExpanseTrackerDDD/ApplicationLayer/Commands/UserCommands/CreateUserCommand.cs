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
        public SecureString Password { get; set; }
        public SecureString RepeatPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
