using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class ProductLinkExportViewModel
    {
        public int ProductId { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public string ProductCode { get; set; }
        public string Color { get; set; }
        public string UOM { get; set; }
        public string Brand { get; set; }
        public string Category { get; set; }


    }
}
