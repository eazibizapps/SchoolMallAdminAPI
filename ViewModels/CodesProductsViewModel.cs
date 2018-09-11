using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class CodesProductsViewModel
    {
		public string Code { get; set; }
		public string CodeDescription { get; set; }
		public bool? Active { get; set; }
		public bool? Linked { get; set; }

	}
}
