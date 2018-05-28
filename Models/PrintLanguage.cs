using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiJwt.Entities;

namespace WebApiJwt.Models
{
    public class PrintLanguages : IPrintLanguages
    {
        private readonly ApplicationDbContext _context;

        public PrintLanguages(ApplicationDbContext context)
        {
            _context = context;
        }


        public string GetSchoolPrintLanguages(int SchoolID) {
            return  _context.Schools.Where(m => m.SchoolId == SchoolID).Select(mm => mm.PrintLanguage).FirstOrDefault();
        }

        public string GetSchoolPrintLanguagesLetter(string lg)
        {
            string letter = "";

            if (lg == "4")
            {
                letter =  _context.SchoolLetterTemplate.Where(m => m.Id == 1).Select(s => s.English).FirstOrDefault();
            }

            if (lg == "3")
            {
                letter = _context.SchoolLetterTemplate.Where(m => m.Id == 1).Select(s => s.Afrikaans).FirstOrDefault();
            }

            if (lg == "5")
            {
                letter = _context.SchoolLetterTemplate.Where(m => m.Id == 1).Select(s => s.Dual).FirstOrDefault();
            }


            return letter;
        }


        public string PrintLanguage { get; set; }
    }
}
