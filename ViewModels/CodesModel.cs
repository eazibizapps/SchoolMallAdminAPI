using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class CodesModel
    {
		public string Type { get; set; }
		public string Code { get; set; }
		public string CodeDescription { get; set; }
		public bool? Active { get; set; }
		public string UserID { get; set; }


	}
}
