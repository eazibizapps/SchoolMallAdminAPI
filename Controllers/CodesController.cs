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
using AutoMapper;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data;

namespace WebApiJwt.Controllers
{
    [Route("[controller]/[action]")]
    public class CodesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CodesController(ApplicationDbContext context)
        {
            _context = context;
        }



		[Authorize]
		[HttpGet]
		public string Max(string type)
		{


			int mx = 0;


			try {

			

			_context.Database.OpenConnection();

			using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
			{
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "sp_CodeMax";


				List<SqlParameter> sp = new List<SqlParameter>()
					{
						 new SqlParameter() {ParameterName = "@Type", SqlDbType = SqlDbType.VarChar, Value = type }

					};

				cmd.Parameters.AddRange(sp.ToArray());

				using (var reader = cmd.ExecuteReader())
				{
						while (reader.Read()) {
							mx = reader["mx"] as int? ?? default(int);
						}
					
				}
			}

			_context.Database.CloseConnection();



			return mx.ToString();

			}
			catch (Exception ex)
			{
                var error = ex.InnerException;
				return "";
			}



		}


		[Authorize]
        [HttpGet]
        public List<CodesViewModel> Code001()
        {

			var codes = from a in _context.Codes
						where a.Type == "001" && a.Active == true
                        select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };

            return codes.OrderBy(a => a.CodeDescription).ToList();
        }




        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code002() {

            var codes = from a in _context.Codes
                    where a.Type == "002" && a.Active == true
						select new CodesViewModel()
                    {
                        Code = a.Code,
                        CodeDescription = a.CodeDescription
                    };

            return codes.OrderBy(a => a.CodeDescription).ToList();
        }

        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code003()
        {
            var codes = from a in _context.Codes
                        where a.Type == "003" && a.Active == true
						select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }
        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code004()
        {
            var codes = from a in _context.Codes
                        where a.Type == "004" && a.Active == true
						select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }

        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code004sub()
        {
            var codes = from a in _context.Codes
                        where a.Type == "004" && (a.Code == "3" || a.Code == "4" || a.Code == "5") && a.Active == true
						select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }

        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code005()
        {
            var codes = from a in _context.Codes
                        where a.Type == "005" && a.Active == true
						select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }


        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code006()
        {
            var codes = from a in _context.Codes
                        where a.Type == "006" && a.Active == true
						select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }


        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code007()
        {
            var codes = from a in _context.Codes
                        where a.Type == "007" && a.Active == true
                        select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }



        [Authorize]
        [HttpPost]
        public bool Code007Update([FromBody] CodesViewModel model)
        {
            Codes dbCode  = _context.Codes.Where(c => c.Type == "007" && c.Code == model.Code).FirstOrDefault();
            dbCode.CodeDescription = model.CodeDescription;
            _context.Codes.Update(dbCode);
            _context.SaveChanges();
            return true;
        }

        [Authorize]
        [HttpPost]
        public bool Code007Delete([FromBody] CodesViewModel model)
        {
            Codes dbCode = _context.Codes.Where(c => c.Type == "007" && c.Code == model.Code).FirstOrDefault();
            dbCode.Active = false;
            _context.Codes.Remove(dbCode);
            _context.SaveChanges();
            return true;
        }



        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code008()
        {
            var codes = from a in _context.Codes
                        where a.Type == "008" && a.Active == true
						select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }

        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code012()
        {
            var codes = from a in _context.Codes
                        where a.Type == "012" && a.Active == true
                        select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }

        [Authorize]
        [HttpPost]
        public bool Code012Update([FromBody] CodesViewModel model)
        {
            Codes dbCode = _context.Codes.Where(c => c.Type == "012" && c.Code == model.Code).FirstOrDefault();
            dbCode.CodeDescription = model.CodeDescription;
            _context.Codes.Update(dbCode);
            _context.SaveChanges();
            return true;
        }

        [Authorize]
        [HttpPost]
        public bool Code012Delete([FromBody] CodesViewModel model)
        {
            Codes dbCode = _context.Codes.Where(c => c.Type == "012" && c.Code == model.Code).FirstOrDefault();
            dbCode.Active = false;
            _context.Codes.Remove(dbCode);
            _context.SaveChanges();
            return true;
        }

        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code009()
        {
            var codes = from a in _context.Codes
                        where a.Type == "009" && a.Active == true
						select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }


        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code010()
        {
            var codes = from a in _context.Codes
                        where a.Type == "010" && a.Active == true
						select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }


		[Authorize]
		[HttpGet]
		public List<CodesViewModel> Code011()
		{
			var codes = from a in _context.Codes
						where a.Type == "011" && a.Active == true
						select new CodesViewModel()
						{
							Code = a.Code,
							CodeDescription = a.CodeDescription
						};
			return codes.OrderBy(a => a.CodeDescription).ToList();
		}



		[Authorize]
		[HttpPost]
		public Boolean Edit([FromBody] CodesModel model)
		{

			try
			{

				var db = _context.Codes.Where(m => m.Type == model.Type && m.Code == model.Code).FirstOrDefault();

				if (db != null)
				{
					Mapper.Map(model, db);
					_context.Codes.Update(db);
					_context.SaveChanges();
					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex) {

				var a = ex.InnerException;
				return false;
			}
		}

		[Authorize]
		[HttpPost]
		public Boolean Add([FromBody] CodesModel model)
		{
			var db = _context.Codes.Where(m => m.Type == model.Type && m.Code == model.Code).FirstOrDefault();

			if (db == null)
			{
				db = new Codes();
				Mapper.Map(model, db);
				_context.Codes.Add(db);
				_context.SaveChanges();
				return true;
			}
			else
			{
				return false;
			}
		}



		[Authorize]
        [HttpGet]
        public List<CodesProductsViewModel> Products()
        {

			List<CodesProductsViewModel> products = new List<CodesProductsViewModel>();
			try
			{
				_context.Database.OpenConnection();

				using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "sp_Products";

			

					using (var reader = cmd.ExecuteReader())
					{
						products = reader.MapToList<CodesProductsViewModel>();
					}
				}

				_context.Database.CloseConnection();

			}
			catch (Exception ex)
			{
                var error = ex.InnerException;
            }

			return products;

			//var codes = from a in _context.SchoolProducts
			//			join b in _context.SchoolProductsLink on a.ProductId equals b.ProductId
			//			//join s in _context.Schools on pk.SchoolId equals s.SchoolId
			//			where a.Active == true
   //                     select new CodesProductsViewModel()
   //                     {
   //                         Code = a.ProductId.ToString(),
   //                         CodeDescription = a.Description,
   //                         Active = a.Active,
			//				Linked = CheckLink(b.ProductCode)
   //                     };

   //         return codes.OrderBy(a => a.CodeDescription).ToList();
        }

		public bool CheckLink(string s) {

			if (s == null)
			{
				return false;
			}

			if (s == "")
			{
				return false;
			}
			
			return true;
		}


    }
}
