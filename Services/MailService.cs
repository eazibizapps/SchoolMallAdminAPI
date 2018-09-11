using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace WebApiJwt.Services
{
    public class MailService
    {
		private string _MailServer;
		private string _UserName;
		private string _Password;
		private string _MailFrom;
		private string _MailTo;
		private string _Body;
		private string _Subject;


		public MailService(string MailServer,string UserName,string Password,string MailFrom,string MailTo,string Body, string Subject)
		{
			_MailServer = MailServer;
			_UserName = UserName;
			_Password = Password;
			_MailFrom = MailFrom;
			_MailTo = MailTo;
			_Body = Body;
			_Subject = Subject;
		}


		public void SendMail()
		{
            try
            {

                SmtpClient client = new SmtpClient(_MailServer);
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(_UserName, _Password);

                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(_MailFrom);
                mailMessage.To.Add(_MailTo);
                //mailMessage.Bcc.Add("gayl@funworld.co.za");

                mailMessage.Body = _Body;
                mailMessage.IsBodyHtml = true;
                mailMessage.Subject = _Subject;
                client.Send(mailMessage);
            }
            catch (Exception ex) {
                var error = ex.InnerException;
            }

			
		}

	}
}
