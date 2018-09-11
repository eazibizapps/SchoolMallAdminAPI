using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class ResetPassword
    {
		public string Id { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
