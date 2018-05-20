using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using WebApiJwt.Models;
using WebApiJwt.ViewModels;
using AutoMapper;

namespace WebApiJwt.Controllers
{

    [Route("[controller]/[action]")]
    public class SchoolGradesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPeriods _IPeriods;

        public SchoolGradesController(ApplicationDbContext context, IPeriods IPeriods)
        {
            _context = context;
            _IPeriods = IPeriods;

        }

        [Authorize]
        [HttpGet]
        public SchoolPeriodViewModel Period() {
           
            return new SchoolPeriodViewModel() {
                CurrentYear = _IPeriods.PeriodYear,
                PreviuosYears = _context.SchoolsPeriods.Where(p => p.PeriodYear < _IPeriods.PeriodYear).Select(s => s.PeriodYear).ToList()
        };


        }

        [Authorize]
        [HttpPost]
        public bool DeleteGradeListPoroduct([FromBody] ScoolGradesListViewModel model)
        {

            ScoolGradesList db = _context.ScoolGradesList.Where(l => l.ScoolGradesListID == model.ScoolGradesListID && l.SchoolId == model.SchoolId && l.ProductId == model.ProductId).FirstOrDefault();

            try
            {

                var result = _context.ScoolGradesList.Remove(db);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }



            return true;
        }




        [Authorize]
        [HttpPost]
        public bool EditGradeListPoroduct([FromBody] ScoolGradesListViewModel model)
        {

            ScoolGradesList db = _context.ScoolGradesList.Where(p => p.SchoolId == model.SchoolId && p.ScoolGradesListID == model.ScoolGradesListID && p.ProductId == model
            .ProductId).FirstOrDefault();
            
            try
            {

                if (db.SchoolId == model.SchoolId && db.ScoolGradesListID == model.ScoolGradesListID && db.ProductId == model.ProductId)
                {
                    if (model.ProducListDescription != null) {
                        if (model.ProducListDescription.Length == 0) {
                            model.ProducListDescription = null;
                        }
                    }

                    Mapper.Map(model, db);
                    var result = _context.ScoolGradesList.Attach(db);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            catch (Exception ex)
            {
                return false;
            }



            return true;
        }


        [Authorize]
        [HttpPost]
        public bool AddGradeListPoroduct([FromBody] ScoolGradesListViewModel model) {

            ScoolGradesList db = _context.ScoolGradesList.Where(p => p.SchoolId == model.SchoolId && p.SchoolGradeId == model.SchoolGradeId && p.ProductId == model.ProductId).FirstOrDefault();

            



            try
            {
                if (db == null)
                {
                    ScoolGradesList savedata = new ScoolGradesList();
                    Mapper.Map(model, savedata);
                    var result = _context.ScoolGradesList.Add(savedata);
                    _context.SaveChanges();
                    return true;
                }
                else {
                    return false;
                }
                
            }
            catch (Exception ex) {
                return false;
            }



            return true;
        }


        [Authorize]
        [HttpPost]
        public bool SchoolGradesDelete([FromBody] SchoolGradesViewModel model)
        {
            try
            {
                ScoolGrades scoolGrades = _context.ScoolGrades.Where(m => m.SchoolId == model.SchoolId && m.SchoolGradeId == model.SchoolGradeId && m.GradeCode == model.GradeCode).FirstOrDefault();
                List<ScoolGradesList> scoolGradesList = _context.ScoolGradesList.Where(m => m.SchoolId == model.SchoolId && m.SchoolGradeId == model.SchoolGradeId).ToList();
                SchoolGradeTotals schoolGradeTotals = _context.SchoolGradeTotals.Where(m => m.SchoolGradeId == model.SchoolGradeId && m.SchoolId == model.SchoolId && m.PeriodId == m.PeriodId).FirstOrDefault();
                scoolGrades.UserID = model.UserID;



                if (schoolGradeTotals != null) {
                    schoolGradeTotals.UserID = model.UserID;
                    _context.SchoolGradeTotals.Remove(schoolGradeTotals);
                }

                _context.SaveChanges();

                if (scoolGradesList != null)
                {
                    if (scoolGradesList.Count() > 0)
                    {
                        foreach (var item in scoolGradesList)
                        {
                            item.UserID = model.UserID;
                            _context.ScoolGradesList.Remove(item);
                        }
                    }
                }

                _context.SaveChanges();

                if (scoolGradesList != null)
                {
                    _context.ScoolGrades.Remove(scoolGrades);
                }

                _context.SaveChanges();

                return true;

            }
            catch (Exception ex) {
                return false;
            }

            
        }


        //SchoolGradesViewModel
        [Authorize]
        [HttpPost]
        public bool SchoolListGradesEdit([FromBody] SchoolGradesViewModel model)
        {
            try
            {


            SchoolGradeTotals schoolGradeTotals = _context.SchoolGradeTotals.Where(m => m.SchoolId == model.SchoolId && m.SchoolGradeId == model.SchoolGradeId && m.PeriodId == _IPeriods.PeriodId).FirstOrDefault();
                schoolGradeTotals.NoOffClasses = model.NoOffClasses;
                schoolGradeTotals.NoOffLearners = model.NoOffLearners;
                schoolGradeTotals.NoOffParticipation = model.NoOffParticipation;
                schoolGradeTotals.UserID = model.UserID;
                


            if (schoolGradeTotals != null)
            {
                
                _context.SchoolGradeTotals.Attach(schoolGradeTotals);
                _context.SaveChanges();
                return true;
            }
            else {
                    schoolGradeTotals.SchoolId = model.SchoolId;
                    schoolGradeTotals.SchoolGradeId = model.SchoolGradeId;
                    schoolGradeTotals.PeriodId = _IPeriods.PeriodId;
                _context.SchoolGradeTotals.Add(schoolGradeTotals);
                _context.SaveChanges();
                return true;
            }


            }
            catch (Exception ex) {
                return false;
            }


        }

        [Authorize]
        [HttpPost]
        public bool SchoolListGradesAdd([FromBody] SchoolGradesViewModel model)
        {
            try
            {
                ScoolGrades existingItem = _context.ScoolGrades.Where(m => m.SchoolId == model.SchoolId && m.SchoolGradeId == model.SchoolGradeId && m.GradeCode == model.GradeCode).FirstOrDefault();

                if (existingItem == null)
                {
                    ScoolGrades db = new ScoolGrades();
                    db.SchoolId = model.SchoolId;
                    db.GradeCode = model.GradeCode;
                    db.UserID = model.UserID;
                    db.Type007 = "007";
                    _context.ScoolGrades.Add(db);
                    _context.SaveChanges();

                    SchoolGradeTotals schoolGradeTotals = _context.SchoolGradeTotals.Where(m => m.SchoolId == model.SchoolId && m.SchoolGradeId == db.SchoolGradeId && db.SchoolId == _IPeriods.PeriodId).FirstOrDefault();

                    if (schoolGradeTotals == null) {
                        SchoolGradeTotals gradeTotals = new SchoolGradeTotals() { NoOffLearners = 0, NoOffClasses = 0, NoOffParticipation = 0, PeriodId = _IPeriods.PeriodId, SchoolGradeId = db.SchoolGradeId, SchoolId = model.SchoolId, UserID = model.UserID };
                        _context.SchoolGradeTotals.Add(gradeTotals);
                        _context.SaveChanges();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex) {
                return false;
            }


            return true;
        }


        [Authorize]
        [HttpPost]
                public List<CodesViewModel> GetGradesFilter([FromBody] SchoolGradesViewModel[] model)
        {
            
            var codes = from a in _context.Codes
                        where a.Type == "007" && a.Active == true && !(from x in model select x.GradeCode).Contains(a.Code)
                        select new CodesViewModel
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription,
                            Active = a.Active
                            
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();


        }

        [Authorize]
        [HttpGet]
        public List<CodesViewModel> GetGrades()
        {

            var codes = from a in _context.Codes
                        where a.Type == "007" && a.Active == true 
                        select new CodesViewModel
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription,
                            Active = a.Active

                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();


        }





        [Authorize]
        [HttpGet]

        public List<SchoolGradesViewModel> GetSchoolGrades(int Id, int Year)
        {
            List<SchoolGradesViewModel> grades = new List<SchoolGradesViewModel>();
            try
            {
                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetSchoolsGrades";

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                         new SqlParameter() {ParameterName = "@SchoolId", SqlDbType = SqlDbType.Int, Value = Id},
                         new SqlParameter() {ParameterName = "@PeriodId", SqlDbType = SqlDbType.Int, Value = _IPeriods.PeriodId}
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        grades = reader.MapToList<SchoolGradesViewModel>();
                    }
                }

                _context.Database.CloseConnection();

            }
            catch (Exception ex) {

            }
            
            return grades;
            
        }

        [Authorize]
        [HttpGet]


        public List<ScoolGradesListViewModel> GetScoolGradesList(int Id) {

            List<ScoolGradesListViewModel> grades = new List<ScoolGradesListViewModel>();
            try
            {
                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_ScoolGradesList";

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                         new SqlParameter() {ParameterName = "@SchoolGradeId", SqlDbType = SqlDbType.Int, Value = Id},
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        grades = reader.MapToList<ScoolGradesListViewModel>();
                    }
                }

                _context.Database.CloseConnection();

            }
            catch (Exception ex)
            {

            }

            return grades;
        }
    }
}