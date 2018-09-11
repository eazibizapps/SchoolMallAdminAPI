using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class JWTViewModel
    {
        public string Token { get; set; }
        public string UserID { get; set; }
        public DateTime ValidTo { get; set; }
        public bool IsValid { get; set; }
        public string Role { get; set; }

    }
}
