using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public interface Isms
    {
        string Authentication();
        string toke { get; set; }


    }
}
