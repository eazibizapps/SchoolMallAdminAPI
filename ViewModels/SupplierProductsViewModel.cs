using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class SupplierProductsViewModel
    {

        public string SuppliersId { get; set; }
        public int? ProductId { get; set; }
        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public decimal? SupplierPrice { get; set; }
        public decimal? RetailPrice { get; set; }
        public string UserID { get; set; }
        public string BrandCode { get; set; }
        public string CategoryCode { get; set; }
        public string UOMCode { get; set; }
        public string CatalogueCode { get; set; }


    }
}
