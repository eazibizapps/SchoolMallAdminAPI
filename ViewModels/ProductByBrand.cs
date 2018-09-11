using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class ProductByBrand
    {
		public string Description { get; set; }
		public int Quantity { get; set; }
		public string ProvinceCode { get; set; }
		public int PeriodYear { get; set; }

	}
}
