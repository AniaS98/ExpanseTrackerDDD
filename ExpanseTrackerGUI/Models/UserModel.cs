using ExpanseTrackerDDD.ApplicationLayer.DTOs;
using System;

namespace ExpanseTrackerGUI.Models
{
    public class UserModel
    {
        //public string RequestId { get; set; }

        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
