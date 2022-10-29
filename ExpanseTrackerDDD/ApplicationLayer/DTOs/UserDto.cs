using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ExpanseTrackerDDD.ApplicationLayer.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("User: " + Id + "\n");
            sb.Append("Login: " + Login + "\n");
            sb.Append("Name: " + FirstName + " " + LastName + "\n");

            return sb.ToString();
        }
    }
}
