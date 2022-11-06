﻿using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.Commands.UserCommands
{
    public class LogInCommand
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
