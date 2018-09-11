using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Entities;

namespace WebApiJwt.Controllers
{
    [Route("[controller]/[action]")]
    public class EmailController : Controller
    {
        private readonly ApplicationDbContext _ApplicationDbContext;

        public EmailController(ApplicationDbContext ApplicationDbContext)
        {
            _ApplicationDbContext = ApplicationDbContext;
        }


        [Authorize]
        [HttpGet]
        public MailTemplateReturn GetEmailTemplate(string template)
        {
            MailTemplateReturn mailTemplateReturn = new MailTemplateReturn();
            mailTemplateReturn.Template = _ApplicationDbContext.EmailTemplates.Where(m => m.TemplateType == template).Select(s => s.Template).FirstOrDefault();

            return mailTemplateReturn;
        }


        [Authorize]
        [HttpPost]
        public bool PostEmailTemplate([FromBody] MailTemplateUpdate template)
        {
            var db = _ApplicationDbContext.EmailTemplates.Where(m => m.TemplateType == template.TemplateType).FirstOrDefault();


            if (db != null)
            {
                db.Template = template.Template;
                _ApplicationDbContext.EmailTemplates.Update(db);
                _ApplicationDbContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

            
        }



    }

    public class MailTemplateReturn
    {
        public string Template { get; set; }
    }

    public class MailTemplateUpdate
    {
        public string Template { get; set; }
        public string TemplateType { get; set; }
    }

}