using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class GradeList
    {
		public string Name { get; set; }
		public int SchoolId { get; set; }
		public string Grade { get; set; }
		public int SchoolGradeId { get; set; }
		public bool Selected { get; set; }

	}
}
