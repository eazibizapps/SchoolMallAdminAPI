using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class OrderStatuss
    {
        public string OrderStatus { get; set; }
        public int Lite_Merchant_Trace { get; set; }
        public string recipientName { get; set; }
        public string deliveryContactNumber { get; set; }
        public string AddressType { get; set; }
        public string complexBuildingDetails { get; set; }
        public string streetAddress { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string OrderDate { get; set; }

    }
}
