﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Entities;
using WebApiJwt.Models;
using WebApiJwt.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using AutoMapper;

namespace WebApiJwt.Controllers
{
    [Route("[controller]/[action]")]
    public class SchoolPressKitController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPeriods _IPeriods;
        private readonly IPrintLanguages _IPrintLanguages;

        public SchoolPressKitController(ApplicationDbContext context, IPeriods IPeriods, IPrintLanguages IPrintLanguages)
        {
            _context = context;
            _IPeriods = IPeriods;
            _IPrintLanguages = IPrintLanguages;

        }

        [Authorize]
        [HttpPost]
        public bool UpdateSchoolPressKit([FromBody] SchoolPressKitViewModel model ) {

            SchoolPressKit db = _context.SchoolPressKit.Where(m => m.SchoolId == model.SchoolId).FirstOrDefault();
            Schools dbSchool = _context.Schools.Where(m => m.SchoolId == model.SchoolId).FirstOrDefault();

            string letter = _IPrintLanguages.GetSchoolPrintLanguagesLetter(model.PrintLanguage);
            byte[] templateLetter = Encoding.Unicode.GetBytes(letter);
            byte[] overLetter = Encoding.Unicode.GetBytes(model.LetterOveride);

            var compare = templateLetter.SequenceEqual(overLetter); // true

            if (compare) {
                model.LetterOveride = null;
            }

            Mapper.Map(model, db);

            if (db != null)
            {
                _context.SchoolPressKit.Update(db);
                _context.SaveChanges();
            }
            else {
                _context.SchoolPressKit.Add(db);
                _context.SaveChanges();
            }


            if (dbSchool.PrintLanguage != model.PrintLanguage) {
                dbSchool.PrintLanguage = model.PrintLanguage;
                _context.Schools.Update(dbSchool);
                _context.SaveChanges();
            }

            
            return true;
        }




        [Authorize]
        [HttpGet]
        public SchoolPressKitViewModel GetSchoolPressKit(int id) {

            var schoolPressKit = (from pk in _context.SchoolPressKit
                    join s in _context.Schools on pk.SchoolId equals s.SchoolId
                    where pk.SchoolId == id
                    select new SchoolPressKitViewModel()
                    {
                        SchoolId = pk.SchoolId,
                        LetterOveride = pk.LetterOveride,
                        Logo = pk.Logo,
                        PrintLanguage = s.PrintLanguage,
                        Signature = pk.Signature,
                        UserID = pk.UserID
                    }).FirstOrDefault();


            if (schoolPressKit.LetterOveride == null) {
                var lg = _IPrintLanguages.GetSchoolPrintLanguages(id);

                if (lg == "4") {
                    schoolPressKit.LetterOveride = _context.SchoolLetterTemplate.Where(m => m.Id == 1).Select(s => s.English).FirstOrDefault();
                }

                if (lg == "3")
                {
                    schoolPressKit.LetterOveride = _context.SchoolLetterTemplate.Where(m => m.Id == 1).Select(s => s.Afrikaans).FirstOrDefault();
                }

                if (lg == "5")
                {
                    schoolPressKit.LetterOveride = _context.SchoolLetterTemplate.Where(m => m.Id == 1).Select(s => s.Dual).FirstOrDefault();
                }
            }

            return schoolPressKit;

        }
        
    }
}