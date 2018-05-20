using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public interface IPeriods
    {
        int PeriodId { get; set; }
        int PeriodYear { get; set; }
    }
}
