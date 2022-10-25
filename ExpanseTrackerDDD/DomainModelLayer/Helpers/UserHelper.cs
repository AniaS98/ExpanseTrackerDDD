using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace ExpanseTrackerDDD.DomainModelLayer.Helpers
{
    public class UserHelper
    {
        public static void VerifyPasswords(string password, string repeatPassword)
        {
            if (password != repeatPassword)
                throw new Exception("Passwords do not match. Please try again");
        }

    }
}
