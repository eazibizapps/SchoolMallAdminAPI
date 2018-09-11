using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class Products
    {
		public bool Selected { get; set; }
		public string ProductCode { get; set; }
		public string Description { get; set; }
		public decimal UnitPrice { get; set; }
		public decimal TotalUnitPrice { get; set; }
		public decimal UnitPriceVAT { get; set; }
		public decimal LineTotalTotal { get; set; }
		public decimal LineTotalVAT { get; set; }
		public int Quantity { get; set; }
		public decimal LineTotal { get; set; }
		public string Grade { get; set; }
		public string Image { get; set; }
		public int ScoolGradesListID { get; set; }
		public int NoOfLearners { get; set; }
	}
}
