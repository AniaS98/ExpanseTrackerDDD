using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.UserCommands
{
    public class LoginCommand
    {
        public string Login { get; set; }
        public SecureString Password { get; set; }
    }
}
