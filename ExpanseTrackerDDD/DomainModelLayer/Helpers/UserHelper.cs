using System;
using System.Collections.Generic;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace ExpanseTrackerDDD.DomainModelLayer.Helpers
{
    public class UserHelper
    {
        // https://blog.codinghorror.com/regular-expressions-now-you-have-two-problems/
        public static void PasswordValidation(string password, string repeatPassword)
        {
            //Hasło powinno zawierać duże i małe litery, cyfrę i znak specjalny (#?!@$%^&*-). Hasło powinno mieć przynajmniej 6 znaków
            Regex validationString = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{6,}$");
            if (validationString.IsMatch(password) == false)
                throw new Exception("The provided password does not meet the requirements. Please try again");

            PasswordVerification(password, repeatPassword);
        }

        public static void PasswordVerification(string password, string repeatPassword)
        {
            //Jeżeli hasła się nie zgadzają, wystąpi wyjątek
            if (password != repeatPassword)
                throw new Exception("Passwords do not match. Please try again");
        }
    }
}
