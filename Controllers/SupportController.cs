using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Entities;
using Microsoft.AspNetCore.Authorization;
using WebApiJwt.ViewModels;
using AutoMapper;

namespace WebApiJwt.Controllers
{
    [Route("[controller]/[action]")]
    public class SupportController : Controller
    {
        private readonly ApplicationDbContext _context;


        public SupportController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpGet]
        public List<SupportTasksViewModel> SupportTasks()
        {
            var supportTasks = from a in _context.SupportTasks

                            select new SupportTasksViewModel()
                            {
                                Id = a.Id,
                                Active = a.Active,
                                Comments = a.Comments,
                                TaskName = a.TaskName,
                                UserID = a.UserID

                            };

            return supportTasks.ToList();
        }


        [Authorize]
        [HttpPost]
        public bool UpdateSupportTasks([FromBody] SupportTasksViewModel model)
        {

            SupportTasks db = _context.SupportTasks.Where(c => c.Id == model.Id).FirstOrDefault();

            try
            {
                if (db.Id == model.Id)
                {

                    Mapper.Map(model, db);
                    _context.SupportTasks.Update(db);
                    var result = _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return false;
            }
        }

        [Authorize]
        [HttpPost]
        public int AddSupportTasks([FromBody] SupportTasksViewModel model)
        {
            SupportTasks db = new SupportTasks();
            model.Active = true;
            try
            {

                    Mapper.Map(model, db);
                    _context.SupportTasks.Attach(db);
                    var result = _context.SaveChanges();
                return db.Id;
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return 0;
            }
        }


        [Authorize]
        [HttpPost]
        public bool RemoveSupportTasks([FromBody] SupportTasksViewModel model)
        {

            SupportTasks db = _context.SupportTasks.Where(c => c.Id == model.Id).FirstOrDefault();

            try
            {
                if (db.Id == model.Id)
                {
                    _context.SupportTasks.Remove(db);
                    var result = _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return false;
            }
        }


    }
}