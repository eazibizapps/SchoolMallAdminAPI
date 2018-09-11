using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Entities;
using WebApiJwt.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using System.Dynamic;
using System.Data.SqlClient;
using System.Data;
using WebApiJwt.Models;
using Newtonsoft.Json;

namespace WebApiJwt.Controllers
{
	[Route("[controller]/[action]")]
	public class ReportsController : Controller
    {
		private readonly ApplicationDbContext _context;


		public ReportsController(ApplicationDbContext context)
		{
			_context = context;
		}

		[Authorize]
		[HttpGet]
		public object GetProductByBrand(string id) {

			string jsone = "";
			try
			{
				_context.Database.OpenConnection();
			
				using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "rpt_Product_By_Brand";

					List<SqlParameter> sp = new List<SqlParameter>()
					{
						 new SqlParameter() {ParameterName = "@Code", SqlDbType = SqlDbType.VarChar, Value = id},
					};
					
					cmd.Parameters.AddRange(sp.ToArray());

					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read()) {

							jsone = reader["Json"].ToString(); 
						}
					}
				}

				var a = JsonConvert.DeserializeObject(jsone); ;

				_context.Database.CloseConnection();

			}
			catch (Exception ex)
			{
                var error = ex.InnerException;
            }
			
			return JsonConvert.DeserializeObject(jsone);  

		}


		[Authorize]
		[HttpGet]
		public List<ProductByBrandNames> GetProductByBrandName(string id)
		{

			List<ProductByBrandNames> list = new List<ProductByBrandNames>();

			try
			{
				_context.Database.OpenConnection();

				using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "rpt_Product_By_Brand_Names";

					List<SqlParameter> sp = new List<SqlParameter>()
					{
						 new SqlParameter() {ParameterName = "@Code", SqlDbType = SqlDbType.VarChar, Value = id},
					};

					cmd.Parameters.AddRange(sp.ToArray());

					using (var reader = cmd.ExecuteReader())
					{
						list = reader.MapToList<ProductByBrandNames>();
					}

				}

				

				_context.Database.CloseConnection();

			}
			catch (Exception ex)
			{
                var error = ex.InnerException;
            }

			return list;

		}

        [Authorize]
        [HttpGet]
        public List<PrittGlueStick43gModel> GetPrittGlueStick43g() {

            List<PrittGlueStick43gModel> list = new List<PrittGlueStick43gModel>();

            try
            {
                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "rpt_PrittGlueStick43g";
                    
                    using (var reader = cmd.ExecuteReader())
                    {
                        list = reader.MapToList<PrittGlueStick43gModel>();
                    }
                }

                _context.Database.CloseConnection();

                return list;

            }
            catch (Exception ex) {
                var err = ex.InnerException;
                return new List<PrittGlueStick43gModel>();
            }




        }

        //ButterflyPocketModel

        [Authorize]
        [HttpGet]
        public List<ButterflyPocketModel> GetButterflyPocket()
        {

            List<ButterflyPocketModel> list = new List<ButterflyPocketModel>();

            try
            {
                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "rpt_ButterflyPocket";

                    using (var reader = cmd.ExecuteReader())
                    {
                        list = reader.MapToList<ButterflyPocketModel>();
                    }
                }

                _context.Database.CloseConnection();

                return list;

            }
            catch (Exception ex)
            {
                var err = ex.InnerException;
                return new List<ButterflyPocketModel>();
            }




        }


        [Authorize]
		[HttpGet]
		public object GetProductByHenkel()
		{

			string jsone = "";
			try
			{
				_context.Database.OpenConnection();

				using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "rpt_Product_Henkel";

					//List<SqlParameter> sp = new List<SqlParameter>()
					//{
					//	 new SqlParameter() {ParameterName = "@Code", SqlDbType = SqlDbType.VarChar, Value = id},
					//};
					//
					//cmd.Parameters.AddRange(sp.ToArray());

					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read())
						{

							jsone = reader["Json"].ToString();
						}
					}
				}

				var a = JsonConvert.DeserializeObject(jsone); ;

				_context.Database.CloseConnection();

			}
			catch (Exception ex)
			{
                var error = ex.InnerException;
            }

			return JsonConvert.DeserializeObject(jsone);

		}


		[Authorize]
		[HttpGet]
		public List<ProductByBrandNames> GetProductByHenkelName()
		{

			List<ProductByBrandNames> list = new List<ProductByBrandNames>();

			try
			{
				_context.Database.OpenConnection();

				using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "rpt_Product_Henkel_Names";

					//List<SqlParameter> sp = new List<SqlParameter>()
					//{
					//	 new SqlParameter() {ParameterName = "@Code", SqlDbType = SqlDbType.VarChar, Value = id},
					//};
					//
					//cmd.Parameters.AddRange(sp.ToArray());

					using (var reader = cmd.ExecuteReader())
					{
						list = reader.MapToList<ProductByBrandNames>();
					}

				}



				_context.Database.CloseConnection();

			}
			catch (Exception ex)
			{
                var error = ex.InnerException;
            }

			return list;

		}





	}
}