using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class ProductLists
    {
		public string Grade { get; set; }
		public string Total { get; set; }
		public int NoOfLearners { get; set; }

		public List<Products> Products;

    }
}
