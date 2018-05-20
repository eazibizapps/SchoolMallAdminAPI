using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class SchoolPeriodViewModel
    {
        public int CurrentYear { get; set; }
        public List<int> PreviuosYears { get; set; }
    }
}
