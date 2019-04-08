using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class TokenModel
    {
        public int UserId { get; set; }
        public string Token { get; set; }
        public string ExpireDateTime { get; set; }
    }
}
