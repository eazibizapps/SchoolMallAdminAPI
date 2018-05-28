using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiJwt.Models
{
    public interface IPrintLanguages
    {
        string PrintLanguage { get; set; }
        string GetSchoolPrintLanguages(int SchoolID);
        string GetSchoolPrintLanguagesLetter(string id);
    }
}
