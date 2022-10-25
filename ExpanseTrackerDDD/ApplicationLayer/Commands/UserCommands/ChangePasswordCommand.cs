using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.UserCommands
{
    public class ChangePasswordCommand
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
