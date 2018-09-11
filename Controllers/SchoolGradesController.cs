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
using RestSharp;
using System.Web;

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


		[HttpPost]
		public bool Success() {

			

			// var client = new RestClient("https://backoffice.nedsecure.co.za/Lite/Transactions/New/EasyAuthorise.aspx");
			// var request = new RestRequest(Method.POST);
			// request.AddHeader("postman-token", "658cc03e-1238-5f03-b818-776b4bbd1b6f");
			// request.AddHeader("cache-control", "no-cache");
			// request.AddHeader("content-type", "application/x-www-form-urlencoded");
			// request.AddParameter("application/x-www-form-urlencoded", "Lite_Merchant_Applicationid=%7B9EBD4A80-3CA5-4968-9004-1A5A827E506E%7D&Lite_Website_Fail_url=http%3A%2F%2Flocalhost%3A58128%2Fapi%2FFail&Lite_Website_TryLater_url=http%3A%2F%2Flocalhost%3A58128%2Fapi%2FTrylater&Lite_Website_Error_url=http%3A%2F%2Flocalhost%3A58128%2Fapi%2FTrylater&Lite_Order_LineItems_Product_1=Donation&Lite_Order_LineItems_Quantity_1=1&Lite_Order_LineItems_Amount_1=1000&Lite_ConsumerOrderID_PreFix=DML&Ecom_BillTo_Online_Email=mathhys.smith%40gmail.com&Ecom_Payment_Card_Protocols=iVeri&Ecom_ConsumerOrderID=AUTOGENERATE&Ecom_TransactionComplete=&Lite_Result_Description=&Lite_Merchant_Trace=800", ParameterType.RequestBody);
			// IRestResponse response = client.Execute(request);

			return true;
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
		public List<ProductLists> GetPublicLists([FromBody] List<int> model){

			var a = model;
			List<ProductLists> productLists = new List<ProductLists>();

			foreach (var item in model)
			{
				List<Products> products = new List<Products>();
				ProductLists productList = new ProductLists();
				try
				{
					_context.Database.OpenConnection();
					using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
					{
						cmd.CommandType = System.Data.CommandType.StoredProcedure;
						cmd.CommandText = "sp_ScoolGradesListPublic";

						List<SqlParameter> sp = new List<SqlParameter>()
					{
						new SqlParameter() {ParameterName = "@SchoolGradeId", SqlDbType = SqlDbType.Int, Value = item},
					};

						cmd.Parameters.AddRange(sp.ToArray());

						using (var reader = cmd.ExecuteReader())
						{
							products = reader.MapToList<Products>();
						}
						
					}
					_context.Database.CloseConnection();

					productList.Grade = products.Select(m => m.Grade).FirstOrDefault();
					productList.NoOfLearners = products.Select(m => m.NoOfLearners).FirstOrDefault();

					productList.Products = products;
					productList.Total = "0";
					productLists.Add(productList);


				}
				catch (Exception ex)
				{
                    var error = ex.InnerException;
                    return new List<ProductLists>();
				}
			}

			return productLists;


		}


		[Authorize]
		[HttpGet]
		public List<GradeList> GetPublicGrades(int id) {

			List<GradeList> gradeList = new List<GradeList>();

			try
			{
				_context.Database.OpenConnection();
				using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "sp_ScoolGradesPublic";

					List<SqlParameter> sp = new List<SqlParameter>()
					{
						new SqlParameter() {ParameterName = "@SchoolId2", SqlDbType = SqlDbType.Int, Value = id},
					};

					cmd.Parameters.AddRange(sp.ToArray());

					

					using (var reader = cmd.ExecuteReader())
					{
						gradeList = reader.MapToList<GradeList>();
					}



				}
				_context.Database.CloseConnection();
				return gradeList;
			}
			catch (Exception ex)
			{
                var error = ex.InnerException;
                return new List<GradeList>(); 
			}

			
		}


		[Authorize]
        [HttpPost]
        public bool DeleteGradeListPoroduct([FromBody] ScoolGradesListViewModel model)
        {

            ScoolGradesList db = _context.ScoolGradesList.Where(l => l.ScoolGradesListID == model.ScoolGradesListID && l.SchoolId == model.SchoolId && l.ProductId == model.ProductId).FirstOrDefault();

            try
            {
				db.Active = false;
				var result = _context.ScoolGradesList.Update(db);

                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return false;
            }
            
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
					db.Active = true;
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
                var error = ex.InnerException;
                return false;
            }


            
        }


		[Authorize]
		[HttpGet]
		public bool Copy(int FromSchoolId,int FromSchoolGradeId, int ToSchoolGradeId) {

			try
			{
				_context.Database.OpenConnection();
				using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "sp_CopyList";

					List<SqlParameter> sp = new List<SqlParameter>()
					{
						new SqlParameter() {ParameterName = "@FromSchoolId", SqlDbType = SqlDbType.Int, Value = FromSchoolId},
						new SqlParameter() {ParameterName = "@FromSchoolGradeId", SqlDbType = SqlDbType.Int, Value = FromSchoolGradeId},
						new SqlParameter() {ParameterName = "@ToSchoolGradeId", SqlDbType = SqlDbType.Int, Value = ToSchoolGradeId},
					};

					cmd.Parameters.AddRange(sp.ToArray());
					cmd.ExecuteNonQuery();


				}
				_context.Database.CloseConnection();
				return true;
			}
			catch (Exception ex)
			{
                var error = ex.InnerException;
                return false;
			}


		}


		[Authorize]
        [HttpPost]
        public bool AddGradeListPoroduct([FromBody] ScoolGradesListViewModel model) {





			ScoolGradesList db = _context.ScoolGradesList.Where(p => p.SchoolId == model.SchoolId && p.SchoolGradeId == model.SchoolGradeId && p.ProductId == model.ProductId).FirstOrDefault();
			if (db != null) {

				_context.ScoolGradesList.Remove(db);
				_context.SaveChanges();
			}
				db = _context.ScoolGradesList.Where(p => p.SchoolId == model.SchoolId && p.SchoolGradeId == model.SchoolGradeId && p.ProductId == model.ProductId).FirstOrDefault();

			





			try
            {
                if (db == null)
                {
						

                    ScoolGradesList savedata = new ScoolGradesList();
                    Mapper.Map(model, savedata);

					if (model.ProducListDescription != null) {
						if (model.ProducListDescription.Trim().Length == 0) {
							savedata.ProducListDescription = null;
						}
					}

					savedata.Active = true;
					savedata.PeriodId = _IPeriods.PeriodId;
					var result = _context.ScoolGradesList.Add(savedata);
                    _context.SaveChanges();
                    return true;
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
					schoolGradeTotals.Active = false;
					_context.SchoolGradeTotals.Update(schoolGradeTotals);
                }

                _context.SaveChanges();

                if (scoolGradesList != null)
                {
                    if (scoolGradesList.Count() > 0)
                    {
                        foreach (var item in scoolGradesList)
                        {
                            item.UserID = model.UserID;
							item.Active = false;

							_context.ScoolGradesList.Update(item);
                        }
                    }
                }

                _context.SaveChanges();

                if (scoolGradesList != null)
                {
					scoolGrades.Active = false;
					_context.ScoolGrades.Update(scoolGrades);
                }

                _context.SaveChanges();

                return true;

            }
            catch (Exception ex) {
                var error = ex.InnerException;
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
                var error = ex.InnerException;
                return false;
            }


        }

        [Authorize]
        [HttpPost]
        public bool SchoolListGradesAdd([FromBody] List<SchoolGradesViewModel> model)
        {
            try
            {
				foreach (var item in model)
				{

					ScoolGrades existingItem = _context.ScoolGrades.Where(m => m.SchoolId == item.SchoolId  && m.GradeCode == item.GradeCode).FirstOrDefault();
					if (existingItem != null) {
						existingItem.Active = true;
						_context.ScoolGrades.Attach(existingItem);
						_context.SaveChanges();
						return true;
					}

					existingItem = _context.ScoolGrades.Where(m => m.SchoolId == item.SchoolId && m.SchoolGradeId == item.SchoolGradeId && m.GradeCode == item.GradeCode).FirstOrDefault();

					if (existingItem == null)
					{
						ScoolGrades db = new ScoolGrades();
						db.SchoolId = item.SchoolId;
						db.GradeCode = item.GradeCode;
						db.UserID = item.UserID;
						db.Type007 = "007";
						db.PeriodId = _IPeriods.PeriodId;
						db.Active = true;
						_context.ScoolGrades.Add(db);
						_context.SaveChanges();

						SchoolGradeTotals schoolGradeTotals = _context.SchoolGradeTotals.Where(m => m.SchoolId == item.SchoolId && m.SchoolGradeId == db.SchoolGradeId && db.SchoolId == _IPeriods.PeriodId).FirstOrDefault();

						if (schoolGradeTotals == null)
						{
							SchoolGradeTotals gradeTotals = new SchoolGradeTotals() { NoOffLearners = 0, NoOffClasses = 0, NoOffParticipation = 0, PeriodId = _IPeriods.PeriodId, SchoolGradeId = db.SchoolGradeId, SchoolId = item.SchoolId, UserID = item.UserID };
							_context.SchoolGradeTotals.Add(gradeTotals);
							_context.SaveChanges();
						}

						
					}
				}
				return true;
            }
            catch (Exception ex) {
                var error = ex.InnerException;
                return false;
            }

            
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
                var error = ex.InnerException;

            }
            
            return grades;
            
        }




        [Authorize]
        [HttpGet]
        public List<ScoolGradesListViewModel> GetScoolGradesList(int Id,string printLanguage) {

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
						 new SqlParameter() {ParameterName = "@printLanguage", SqlDbType = SqlDbType.VarChar, Value = printLanguage}
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
                var error = ex.InnerException;
            }

            return grades;
        }

		public List<GradeViewModel> GetScoolGradesListPrint(int Id, int PeriodId)
		{

			List<GradeViewModel> grades = new List<GradeViewModel>();
			try
			{
				_context.Database.OpenConnection();

				using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "sp_ScoolGradesListPrint";

					List<SqlParameter> sp = new List<SqlParameter>()
					{
						 new SqlParameter() {ParameterName = "@SchoolGradeId", SqlDbType = SqlDbType.Int, Value = Id},
						 new SqlParameter() {ParameterName = "@PeriodId", SqlDbType = SqlDbType.Int, Value = PeriodId}

					};

					cmd.Parameters.AddRange(sp.ToArray());

					using (var reader = cmd.ExecuteReader())
					{
						grades = reader.MapToList<GradeViewModel>();
					}
				}

				_context.Database.CloseConnection();

			}
			catch (Exception ex)
			{
                var error = ex.InnerException;
            }

			return grades;
		}

	}
}