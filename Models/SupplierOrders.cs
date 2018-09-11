using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class SupplierOrders
    {
        public string UserID { get; set; }
        public string OrderDate { get; set; }
        public string Total { get; set; }
        public string OrderNumber { get; set; }

    }
}
