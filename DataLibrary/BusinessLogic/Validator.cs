using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace DataLibrary.BusinessLogic
{
    public static class Validator
    {
        public static int ValidateUser(string username, string password) // 0 = Validated 1 = invalid username 2 = invalid password
        {
            return ValidateUsername(username) ? ValidatePassword(password) ? 0 : 2 : 1;
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
