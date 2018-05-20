using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class SchoolProductsViewModel
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }
        public string DescriptionAfrikaans { get; set; }
        public string DescriptionDual { get; set; }
        public bool Active { get; set; }
        public string CategoryCode { get; set; }


    }
}
