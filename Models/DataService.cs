using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class DataService : IDataService
    {
        public DataService()
        {
            Year = 2018;
        }

        public int Year { get; set; }
    }
}
