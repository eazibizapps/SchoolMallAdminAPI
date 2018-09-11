using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Entities;
using WebApiJwt.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using WebApiJwt.ViewModels;

namespace WebApiJwt.Controllers
{
	[Route("[controller]/[action]")]
	public class PeriodsController : Controller
    {
		private readonly ApplicationDbContext _context;
		private readonly IPeriods _periods;

		public PeriodsController(ApplicationDbContext Context, IPeriods IPeriods)
		{
			_context = Context;
			_periods = IPeriods;
		}

		[Authorize]
		[HttpGet]
		public List<PeriodViewModel> GetPeriod() {

			List<PeriodViewModel> periodViewModel = new List<PeriodViewModel>();

			_context.Database.OpenConnection();

			using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
			{
				cmd.CommandType = System.Data.CommandType.StoredProcedure;
				cmd.CommandText = "sp_GetPeriodReport";

		

				using (var reader = cmd.ExecuteReader())
				{
					periodViewModel = reader.MapToList<PeriodViewModel>();
				}
			}

			_context.Database.CloseConnection();


			return periodViewModel;
		}



	}
}