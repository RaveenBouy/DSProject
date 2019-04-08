using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DataLibrary.BusinessLogic
{
    public static class Validator
    {
        public static int ValidateUser(string username, string password) // 7 = Validated 2 = invalid username 3 = invalid password
        {
            return ValidateUsername(username) ? ValidatePassword(password) ? 7 : 3 : 2;
        }

        private static bool ValidateUsername(string username)
        {
            try
            {
                return Regex.IsMatch(username, @"/^[a-zA-Z0-9]+([_-]?[a-zA-Z0-9])*$/") ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static bool ValidatePassword(string password)
        {
            try
            {
                return Regex.IsMatch(password, @"^[a-zA-Z0-9-_%£$@/\\]*$") ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
