using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiJwt.ViewModels;

namespace WebApiJwt.Models
{
    public class OrderDetail
    {
        public List<OrderPayment> OrderPayment { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }
        public List<OrderAddress> OrderAddress { get; set; }



    }

    public class OrderPayment {
        public string UserID { get; set; }
        public string PaymentType { get; set; }
        public string OrderDate { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderNumber { get; set; }
        public string OrderStatus { get; set; }
        public decimal Total { get; set; }
        public decimal DeliveryFee_Excl { get; set; }
        public decimal DeliveryFee { get; set; }


    }

    public class OrderDetails
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int NoOfLearners { get; set; }
        public decimal SupplierPrice { get; set; }
        public decimal Total { get; set; }
        public bool Selected { get; set; }

    }

    public class OrderAddress
    {
        public string recipientName { get; set; }
        public string deliveryContactNumber { get; set; }
        public string AddressType { get; set; }
        public string complexBuildingDetails { get; set; }
        public string streetAddress { get; set; }
        public string suburb { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }


    }
}
