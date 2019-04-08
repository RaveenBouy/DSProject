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
            var userModel = new UserModel();
            bool isError = false;
            var user = LoadUser(username);

            try
            {
                userModel = user[0];
            }
            catch (Exception)
            {
                isError = true;
            }

            return ((!isError ? user.Count == 1 ? Hashing.ValidatePassword(password, userModel.Password) ? 6 : 3 : 2 : 4), userModel);
        }

        public static bool VerifyToken(string token)
        {
            return TokenProcessor.VerifyToken(token) ? true : false;
        }
    }
}
