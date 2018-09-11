using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApiJwt.Entities;
using Microsoft.AspNetCore.Authorization;
using WebApiJwt.ViewModels;
using System.Data.Common;
using static WebApiJwt.Entities.DataReaderExtensions;
using WebApiJwt.Models;
using System.Data.SqlClient;
using System.Data;
using AutoMapper;

namespace WebApiJwt.Controllers
{
    [Route("[controller]/[action]")]
    public class SchoolsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPeriods _IPeriods;

        public SchoolsController(ApplicationDbContext context, IPeriods IPeriods)
        {
            _context = context;
            _IPeriods = IPeriods;
        }


        [HttpGet]
        [Authorize]
        public string GetschoolLanguage(int id)
        {
            return _context.Schools.Where(s => s.SchoolId == id).Select(a => a.Language).FirstOrDefault();
        }


        [HttpPost]
        [Authorize]
        public List<SchoolSearchReturnViewModel> SchoolsSearch([FromBody] SchoolSearchViewModel model) {


            List<SchoolSearchReturnViewModel> schoolsList = new List<SchoolSearchReturnViewModel>();
            try
            {
                
                _context.Database.OpenConnection();
                
                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SchoolsSearch";
                
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@PeriodId", SqlDbType = SqlDbType.Int, Value = _IPeriods.PeriodId},
                        new SqlParameter() {ParameterName = "@PeriodYear", SqlDbType = SqlDbType.Int, Value = _IPeriods.PeriodYear},
                    };
                
                    cmd.Parameters.AddRange(sp.ToArray());
                
                    using (var reader = cmd.ExecuteReader())
                    {
                        schoolsList = reader.MapToList<SchoolSearchReturnViewModel>();
                    }
                
                }
                
                _context.Database.CloseConnection();

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new List<SchoolSearchReturnViewModel>();
            }

            if (model.Name != null) {
                schoolsList = schoolsList.Where(s => s.Name.ToUpper().Contains(model.Name.ToUpper())).ToList();
            }

            if (model.Category != null)
            {
                schoolsList = schoolsList.Where(s => s.CategoryCode == model.Category).ToList();
            }

            if (model.Type != null)
            {
                schoolsList = schoolsList.Where(s => s.TypeCode == model.Type).ToList();
            }

            if (model.Province != null)
            {
                schoolsList = schoolsList.Where(s => s.ProvinceCode == model.Province).ToList();
            }

            if (model.Status != null)
            {
                schoolsList = schoolsList.Where(s => s.StatusCode == model.Status).ToList();
            }

            return schoolsList;
        }


        [HttpGet]
        [Authorize]
        public List<SchoolListReturnViewModel> SchoolsList()
        {


            List<SchoolListReturnViewModel> schoolsList = new List<SchoolListReturnViewModel>();
            try
            {

                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SchoolsList";

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@PeriodId", SqlDbType = SqlDbType.Int, Value = _IPeriods.PeriodId},
                        new SqlParameter() {ParameterName = "@PeriodYear", SqlDbType = SqlDbType.Int, Value = _IPeriods.PeriodYear},
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        schoolsList = reader.MapToList<SchoolListReturnViewModel>();
                    }

                }

                _context.Database.CloseConnection();

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new List<SchoolListReturnViewModel>();
            }

           

            return schoolsList;
        }


        [HttpGet]
        [Authorize]
        public List<SchoolListReturnViewModel2> SchoolsListSM()
        {


            List<SchoolListReturnViewModel2> schoolsList = new List<SchoolListReturnViewModel2>();
            try
            {

                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SchoolsListSM";

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@PeriodId", SqlDbType = SqlDbType.Int, Value = _IPeriods.PeriodId},
                        new SqlParameter() {ParameterName = "@PeriodYear", SqlDbType = SqlDbType.Int, Value = _IPeriods.PeriodYear},
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        schoolsList = reader.MapToList<SchoolListReturnViewModel2>();
                    }

                }

                _context.Database.CloseConnection();

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new List<SchoolListReturnViewModel2>();
            }



            return schoolsList;
        }




        [HttpGet]
        [Authorize]
        public SchoolsViewModel Details(int id)
        {

            if (id == 0) {
                return new SchoolsViewModel();
            }


            List<SchoolsViewModel> ls = new List<SchoolsViewModel>();

            _context.Database.OpenConnection();

            using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandText = "sp_SchoolDetail";

                List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@PeriodId", SqlDbType = SqlDbType.Int, Value = _IPeriods.PeriodId},
                        new SqlParameter() {ParameterName = "@PeriodYear", SqlDbType = SqlDbType.Int, Value = _IPeriods.PeriodYear},
                        new SqlParameter() {ParameterName = "@id", SqlDbType = SqlDbType.Int, Value = id},


                    };

                cmd.Parameters.AddRange(sp.ToArray());

                using (var reader = cmd.ExecuteReader())
                {
                    ls = reader.MapToList<SchoolsViewModel>();
                }

                if (ls != null) {
                    foreach (var item in ls)
                    {
                        if (item.SchoolId == 0)
                        {
                            item.SchoolId = id;
                        }
                    }
                }
                


            }

            _context.Database.CloseConnection();
            
            if (ls == null)
            {
                return new SchoolsViewModel();
            }

            return ls.FirstOrDefault();
        }


        public List<SchoolBoxReturnViewModel> GetSchoolCodes() {

            var qry = from a in _context.Schools
                      where a.Deleted == false
                      select new SchoolBoxReturnViewModel()
                      {
                          Name = a.Name,
                          SchoolId = a.SchoolId
                      };

           return qry.ToList();

        }


        // GET: Schools
        public async Task<IActionResult> Index()
        {
            return View(await _context.Schools.ToListAsync());
        }

        [HttpGet]
        [Authorize]
        public List<SchoolsViewModel> GetSchools() {

            List<SchoolsViewModel> schoolsList = new List<SchoolsViewModel>();
            try {

                _context.Database.OpenConnection();
                DbCommand cmd = _context.Database.GetDbConnection().CreateCommand();
                cmd.CommandText = "sp_GetSchools";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                using (var reader = cmd.ExecuteReader())
                {
                    schoolsList = reader.MapToList<SchoolsViewModel>();
                }
                _context.Database.CloseConnection();
             
            }
            catch (Exception ex) {
                var error = ex.InnerException;
            }

            return schoolsList;
        }


        [HttpPost]
        [Authorize]
        public List<SchoolsViewModel> GetSchoolsByName([FromBody] SchoolSearchViewModel model) {

            var schools = from s in _context.Schools
                          where s.Name.Contains(model.Name) || s.Representative.Contains(model.Name)
                          select new SchoolsViewModel()
                          {
                              Name = s.Name,
                              Representative = s.Name
                          };

            return schools.ToList();
        }



        // GET: Schools/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Schools/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SchoolId,Name,Reg_number,Type004,Language,Principal,Principal_secretary,Forum_area,Logo,Signature,Representative,Cover_letter,Letter_file,Store_learners,Store_school,Butterfly,Type002,Category,Type003,Type,Deleted,PeriodId,UserID")] Schools schools)
        {
            if (ModelState.IsValid)
            {
                _context.Add(schools);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(schools);
        }

        [HttpPost]
        [Authorize]
        public SchoolSearchReturnViewModel Add([FromBody] SchoolsViewModel model)
        {
            try
            {
                Schools schools = new Schools();
                model.PeriodId = _IPeriods.PeriodId;
                model.Active = true;

                schools.Type002 = "002";
                schools.Type003 = "003";
                schools.Type004 = "004";
                schools.Type006 = "006";
                Mapper.Map(model, schools);
                _context.Schools.Attach(schools);
                _context.SaveChanges();

                _context.SchoolsStatus.Attach(new SchoolsStatus()
                {
                    PeriodId = _IPeriods.PeriodId,
                    Type001 = "001",
                    PeriodYear = _IPeriods.PeriodYear,
                    SchoolId = schools.SchoolId,
                    StatusCode = model.StatusCode,
                    UserID = model.UserID,
                });

                _context.SaveChanges();


                SchoolSearchReturnViewModel school = (from s in _context.Schools
                             where s.SchoolId == schools.SchoolId
                             select new SchoolSearchReturnViewModel
                             {
                                 SchoolId = s.SchoolId,
                                 Name = s.Name
                             }).First();

                return school;
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new SchoolSearchReturnViewModel();
            }
        }


        [HttpPost]
        [Authorize]
        public bool Edit([FromBody] SchoolsViewModel model)
        {
            try
            {
                Schools db = _context.Schools.Where(s => s.SchoolId == model.SchoolId).FirstOrDefault();
                SchoolsStatus dbSchoolsStatus = _context.SchoolsStatus.Where(s => s.PeriodId == _IPeriods.PeriodId && s.PeriodYear == _IPeriods.PeriodYear && s.SchoolId == model.SchoolId).FirstOrDefault();
                SchoolsComments schoolsComments = _context.SchoolsComments.Where(s => s.PeriodId == _IPeriods.PeriodId && s.SchoolId == model.SchoolId).FirstOrDefault();


                if (model.Comments != null) {

                    if (schoolsComments == null)
                    {
                        schoolsComments = new SchoolsComments();
                        schoolsComments.Comments = model.Comments;
                        schoolsComments.UserID = model.UserID;
                        schoolsComments.SchoolId = model.SchoolId;
                        schoolsComments.PeriodId = _IPeriods.PeriodId;
                        _context.SchoolsComments.Add(schoolsComments);
                    }
                    else {
                        schoolsComments.Comments = model.Comments;
                        schoolsComments.UserID = model.UserID;
                        _context.SchoolsComments.Attach(schoolsComments);
                    }
                    _context.SaveChanges();
                }

                if (dbSchoolsStatus != null)
                {
                    if (dbSchoolsStatus.SchoolId == model.SchoolId)
                    {
                        Mapper.Map(model, dbSchoolsStatus);
                        dbSchoolsStatus.Type001 = "001";
                        dbSchoolsStatus.PeriodId = _IPeriods.PeriodId;
                        dbSchoolsStatus.PeriodYear = _IPeriods.PeriodYear;
                        _context.SchoolsStatus.Update(dbSchoolsStatus);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    _context.SchoolsStatus.Add(new SchoolsStatus()
                    {
                        PeriodId = _IPeriods.PeriodId,
                        PeriodYear = _IPeriods.PeriodYear,
                        SchoolId = model.SchoolId,
                        StatusCode = model.StatusCode,
                        UserID = model.UserID,
                    });
                    _context.SaveChanges();
                }

                if (db.SchoolId == model.SchoolId)
                {
                    Mapper.Map(model, db);
                    db.PeriodId = _IPeriods.PeriodId;
                 
                    _context.Schools.Update(db);
                    return  _context.SaveChanges() == 1 ? true : false;
                }
                else {
                    return false;
                }

            }
            catch (Exception ex) {
                var error = ex.InnerException;
                return false;
            }
        }


        // GET: Schools/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schools = await _context.Schools
                .SingleOrDefaultAsync(m => m.SchoolId == id);
            if (schools == null)
            {
                return NotFound();
            }

            return View(schools);
        }

        // POST: Schools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schools = await _context.Schools.SingleOrDefaultAsync(m => m.SchoolId == id);
            _context.Schools.Remove(schools);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SchoolsExists(int id)
        {
            return _context.Schools.Any(e => e.SchoolId == id);
        }
    }
}
