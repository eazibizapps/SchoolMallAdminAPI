using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.ViewModels
{
    public class SchoolGradesViewModel
    {
        public int SchoolGradeId { get; set; }
        public int SchoolId { get; set; }
        public string GradeCode { get; set; }
        public string CodeDescription { get; set; }
        public int NoOffClasses { get; set; }
        public int NoOffLearners { get; set; }
        public int NoOffParticipation { get; set; }
        public int SchoolGradeTotalsID { get; set; }
        public string UserID { get; set; }

    }
}
