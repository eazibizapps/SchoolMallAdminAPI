using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public class SMS : Isms
    {
        public string toke { get; set; }

        public SMS()
        {
            toke = Authentication();
        }

        

        public string Authentication()
        {
            return "";

        }
    }
}
