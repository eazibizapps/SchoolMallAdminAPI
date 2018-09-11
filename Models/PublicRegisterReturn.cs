using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class PublicRegisterReturn
    {
		public bool Valid { get; set; }
		public bool ValidSchoolCode { get; set; }
		public bool ValidEmail { get; set; }
		public	string	Error { get; set; }
	}
}
