using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiJwt.Entities;
using WebApiJwt.Models;

namespace WebApiJwt.Controllers
{
    [Route("[controller]/[action]")]
    public class PublicUsersController : Controller
    {
        private readonly ApplicationDbContext _ApplicationDbContext;
        public PublicUsersController(ApplicationDbContext ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }


        //PublicOrderStatus

        [Authorize]
        [HttpGet]
        public List<PublicOrderStatus> GetOrderStatus(string id)
        {
            try
            {
                List<PublicOrderStatus> publicOrderStatuses = new List<PublicOrderStatus>();

                _ApplicationDbContext.Database.OpenConnection();

                using (DbCommand cmd = _ApplicationDbContext.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_PublicOrderStatus";

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                         new SqlParameter() {ParameterName = "@UserID", SqlDbType = SqlDbType.VarChar, Value = id},
                         
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        publicOrderStatuses = reader.MapToList<PublicOrderStatus>();
                    }
                }

                _ApplicationDbContext.Database.CloseConnection();

                return publicOrderStatuses;
            }
            catch (Exception ex)
            {
                var err = ex.InnerException;
                return new List<PublicOrderStatus>();
            }

        }



        [Authorize]
        [HttpGet]
        public PublicRegister GetUser(string id)
        {
            try
            {

                var useist = _ApplicationDbContext.Users.Where(m => m.Email == id).FirstOrDefault();
                PublicRegister publicRegister = new PublicRegister() {
                    Cell = useist.Cell,
                    Email = useist.Email,
                    FirstName = useist.FirstName,
                    LastName = useist.LastName,
                    SchoolCode = int.Parse(useist.SchoolCode),
                    SchoolName = useist.SchoolName

                };
                
                return publicRegister;
            }
            catch (Exception ex) {
                var err = ex.InnerException;
                return new PublicRegister();
            }
            
        }


        [Authorize]
        [HttpPost]
        public bool UpdatePuplicUser([FromBody] UserUpdate data)
        {

            try
            {
                var dbUser = _ApplicationDbContext.Users.Where(m => m.Email == data.Email).FirstOrDefault();
                dbUser.Cell = data.Cell;
                dbUser.Email = data.Email;
                dbUser.FirstName = data.FirstName;
                dbUser.LastName = data.LastName;
                dbUser.SchoolCode = data.SchoolCode;
                dbUser.SchoolName = data.SchoolName;
                _ApplicationDbContext.Users.Update(dbUser);
                _ApplicationDbContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                var err = ex.InnerException;
                return false;
            }

        }

    }
}