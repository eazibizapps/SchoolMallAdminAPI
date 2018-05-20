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
        public List<CodesViewModel> Code001()
        {

            var codes = from a in _context.Codes
                        where a.Type == "001"
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
                    where a.Type == "002"
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
                        where a.Type == "003"
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
                        where a.Type == "004"
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
                        where a.Type == "005"
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
                        where a.Type == "006"
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
                        where a.Type == "008"
                        select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }

        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Code009()
        {
            var codes = from a in _context.Codes
                        where a.Type == "009"
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
                        where a.Type == "010"
                        select new CodesViewModel()
                        {
                            Code = a.Code,
                            CodeDescription = a.CodeDescription
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }


        [Authorize]
        [HttpGet]
        public List<CodesViewModel> Products()
        {
            var codes = from a in _context.SchoolProducts
                        where a.Active == true
                        select new CodesViewModel()
                        {
                            Code = a.ProductId.ToString(),
                            CodeDescription = a.Description,
                            Active = a.Active
                            
                        };
            return codes.OrderBy(a => a.CodeDescription).ToList();
        }

    }
}
