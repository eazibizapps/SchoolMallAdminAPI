using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class PublicJWTViewModel
    {
		public string Token { get; set; }
		public string UserID { get; set; }
		public DateTime ValidTo { get; set; }
		public bool IsValid { get; set; }
		public string Role { get; set; }
		public string SchoolCode { get; set; }
		public string School { get; set; }

	}
}
