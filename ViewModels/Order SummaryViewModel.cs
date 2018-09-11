using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class Order_SummaryViewModel
    {
        public string UserID { get; set; }
        public string PaymentType { get; set; }
        public string OrderDate { get; set; }
        public string OrderNumber { get; set; }
        public string List_Total_Excluding { get; set; }
        public string DeliveryFee_Excluding { get; set; }
        public string List_Total_Vat { get; set; }
        public string DeliveryFee_VAT { get; set; }
        public string Total { get; set; }
        public string SP_Total_Excluding { get; set; }
        public string SP_VAT { get; set; }
        public string SP_Total { get; set; }
        public string Lite_Merchant_Trace { get; set; }
        public string PaymentStatus { get; set; }

    }
}
