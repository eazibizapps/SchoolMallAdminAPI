using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class SchoolProductsLinkViewModel
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public int SuppliersId { get; set; }
        public string UserID { get; set; }
    }
}
