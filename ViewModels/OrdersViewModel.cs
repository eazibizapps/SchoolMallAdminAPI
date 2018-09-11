using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class OrdersViewModel
    {
		public string UserID { get; set; }
		public string PaymentType { get; set; }
		public string OrderDate { get; set; }
		public string PaymentStatus { get; set; }
		public string OrderNumber { get; set; }
		public string SmTotal { get; set; }
        public string SupliersTotal { get; set; }
        public string Profit { get; set; }


    }
}
