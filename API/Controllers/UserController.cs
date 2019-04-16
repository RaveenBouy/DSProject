using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using DataLibrary.Models;
using static DataLibrary.BusinessLogic.Validator;
using static DataLibrary.BusinessLogic.Verifier;

namespace API.Controllers
{
    [ApiController]
    public class UserController : Controller
    {
        [HttpGet("api/login")]
        public AuthResponseModel LoginUser([FromQuery] string username, string password)
        {
            UserLoginLogic login = new UserLoginLogic();
            return login.Login(username, password);
        }

        [HttpPost("api/register")]
        public AuthResponseModel RegisterUser([FromBody] UserModel userModel)
        {
            UserRegisterLogic logic = new UserRegisterLogic();
            return logic.UserRegister(userModel);
        }

        [HttpDelete("api/staff/deleteuser{token}/{id}")]
        public AuthResponseModel DeleteUser(string token, int id)
        {
            DeleteUserLogic deleteUser = new DeleteUserLogic();
            return deleteUser.DeleteUser(token, id);
        }
    }
}