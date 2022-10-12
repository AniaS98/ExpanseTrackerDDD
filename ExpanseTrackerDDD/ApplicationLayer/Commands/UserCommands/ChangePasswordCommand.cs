using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.UserCommands
{
    public class ChangePasswordCommand
    {
        public Guid Id { get; set; }
        public SecureString Password { get; set; }
        public SecureString RepeatPassword { get; set; }
    }
}
