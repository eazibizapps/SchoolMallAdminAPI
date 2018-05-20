using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using WebApiJwt.Entities;

namespace WebApiJwt.Models
{
    public class Periods : IPeriods
    {
        private readonly ApplicationDbContext _context;

        public Periods(ApplicationDbContext context)
        {
            _context = context;
            _context.Database.OpenConnection();
            DbCommand cmd = _context.Database.GetDbConnection().CreateCommand();
            cmd.CommandText = "sp_GetPeriod";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;


            using (DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    PeriodId = reader["PeriodId"] as int? ?? default(int);
                    PeriodYear = reader["PeriodYear"] as int? ?? default(int);
                }
            }


            _context.Database.CloseConnection();

        }

        public int PeriodId { get; set; }
        public int PeriodYear { get; set; }
    }
}
