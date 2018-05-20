using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Entities;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using System.Diagnostics;
using WebApiJwt.ViewModels;

namespace WebApiJwt.Controllers
{
    [Route("[controller]/[action]")]
    public class SchoolContactsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolContactsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public List<SchoolContacts> Details(int id)
        {
            try
            {
                return _context.SchoolContacts.Where(s => s.SchoolId == id).ToList();
            }
            catch (Exception ex) {
                return new List<SchoolContacts>();
            }
        }

        [Authorize]
        [HttpGet]
        public SchoolContacts Contact(int id)
        {
            try
            {
                return _context.SchoolContacts.Where(s => s.ID == id).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new SchoolContacts();
            }
        }


        [Authorize]
        [HttpPost]
        public bool Edit([FromBody] SchoolContectUpdate model)
        {
            SchoolContacts db = _context.SchoolContacts.Where(c => c.ID == model.ID).FirstOrDefault();

            try
            {
                if (db.ID == model.ID)
                {
                    Mapper.Map(model, db);
                     _context.SchoolContacts.Update(db);
                    var result = _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Authorize]
        [HttpPost]
        public bool Create([FromBody] SchoolContacts model)
        {

            try
            {
                    _context.SchoolContacts.Add(model);
                    return _context.SaveChanges() == 1 ? true : false;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        [Authorize]
        [HttpGet]
        public bool Delete(int id)
        {
            SchoolContacts db = _context.SchoolContacts.Where(c => c.ID == id).FirstOrDefault();

            try
            {
                _context.SchoolContacts.Remove(db);
                return _context.SaveChanges() == 1 ? true : false;
            }
            catch (Exception ex)
            {
                return true;
            }
        }

    }
}