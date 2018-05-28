using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class SchoolPressKitViewModel
    {
        public int SchoolId { get; set; }
        public string LetterOveride { get; set; }
        public string Logo { get; set; }
        public string Signature { get; set; }
        public string PrintLanguage { get; set; }

        public string UserID { get; set; }

    }
}
