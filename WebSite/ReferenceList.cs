using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.BusinessLogic
{
    public static class ReferenceList
    {
		public static string IpAddr { get; set; } = "https://localhost:44376/";
		public static string Book { get; set; } = $"{IpAddr}/api/book/";
		public static string UserRegister { get; set; } = $"{IpAddr}/api/register/";

	}
}
