using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class SchoolSearchReturnViewModel
    {
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string CategoryCode { get; set; }
        public string Type { get; set; }
        public string TypeCode { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string Province { get; set; }
        public string ProvinceCode { get; set; }

    }
}
