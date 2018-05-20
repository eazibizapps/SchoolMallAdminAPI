using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class ResetUserPasswordViewModel
    {
        public string Id { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
