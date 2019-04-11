using System;
using System.Collections.Generic;
using System.Text;
using DataLibrary.DataAccess;
using DataLibrary.Models;

namespace DataLibrary.BusinessLogic
{
    public static class UserProcessor
    {
        public static List<UserModel> LoadUser(string username)
        {
            var sql = $@"SELECT Id, UserName, Email, Pass FROM user WHERE UserName = '{username}'";
            return SqlDataAccess.LoadData<UserModel>(sql);
        }
    }
}
