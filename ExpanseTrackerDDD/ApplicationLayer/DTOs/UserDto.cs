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
        public SecureString Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
