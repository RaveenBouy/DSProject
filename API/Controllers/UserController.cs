using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataLibrary.Models;
using static DataLibrary.BusinessLogic.Validator;
using static DataLibrary.BusinessLogic.Verifier;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public AuthResponseModel Get([FromQuery] string username, string password)
        {

        }

        private int VerifyInput(string username, string password)
        {
            return VerifyUser(username, password);
        }

        private int ValidateInput(string username, string password)
        {
            return ValidateUser(username, password);
        }

        private string GenerateUserToken()
        {

        }
    }
}