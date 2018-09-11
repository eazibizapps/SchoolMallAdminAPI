using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class OrderDetailViewModel
    {
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice_Excl { get; set; }
        public decimal UnitPrice_VAT { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int NoOfLearners { get; set; }
        public decimal LineTotal_Excl { get; set; }
        public decimal LineTotal { get; set; }
        public string Grade { get; set; }
        public string SupplierCode { get; set; }
        public decimal SupplierPrice_Excluding { get; set; }
        public decimal SupplierPrice { get; set; }
        public int SupplierQuantity { get; set; }
        public decimal SupplierLineTotal { get; set; }
        public bool Selected { get; set; }


    }
}
