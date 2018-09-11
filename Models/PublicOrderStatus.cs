using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class PublicOrderStatus
    {
        public int OrderNo { get; set; }
        public string Status { get; set; }
        public string Amount { get; set; }

    }
}
