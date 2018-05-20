using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class SchoolContectUpdate
    {
        public int ID { get; set; }
        public int SchoolId { get; set; }
        public string Name { get; set; }
        public string PositionCode { get; set; }
        public string LandLine { get; set; }
        public string Cell { get; set; }
        public string UserID { get; set; }

    }
}
