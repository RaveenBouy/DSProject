using System;
using System.Collections.Generic;
using System.Text;
using DataLibrary.Models;
using static DataLibrary.BusinessLogic.UserProcessor;

namespace DataLibrary.BusinessLogic
{
    public static class Verifier
    {
        public static (int, UserModel) VerifyeUser(string username, string password)
        {
            var user = LoadUser(username);
            return ((user.Count == 1 ? Hashing.ValidatePassword(password, user[0].Password) ? 6 : 3 : 2), user[0]);
        }

        public static bool VerifyToken(string token)
        {
            return TokenProcessor.VerifyToken(token) ? true : false;
        }
    }
}
