using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using WebApiJwt.Entities;
using Microsoft.AspNetCore.Hosting;
using RestSharp;
using System.Text;
using System.IO;
using HtmlAgilityPack;

namespace WebApiJwt.Controllers
{
    [Produces("application/json")]
    [Route("api/Fail")]
    public class FailController : Controller
    {
		private readonly ApplicationDbContext _context;
		private static IHostingEnvironment _env;

		public FailController(ApplicationDbContext context, IHostingEnvironment env)
		{
			_context = context;
			_env = env;
		}

		[HttpPost]
		public bool Fail(Update update)
		{
			string url = "http://localhost:4200/#/order/fail/?id=";

			if (_env.IsProduction())
			{
				url = "http://www.schoolmall.co.za/#/order/fail/?id=";
			}
			else
			{
				url = "http://localhost:4200/#/order/fail/?id=";
			}



			var client = new RestClient("https://portal.nedsecure.co.za/Lite/AuthoriseInfo.aspx");
			var request = new RestRequest(Method.POST);
			object p = "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"Lite_Merchant_ApplicationId\"\r\n\r\n{9EBD4A80-3CA5-4968-9004-1A5A827E506E}\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"Lite_Merchant_Trace\"\r\n\r\n" + update.Lite_Merchant_Trace + "\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--";

			request.AddHeader("cache-control", "no-cache");
			request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
			request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", p, ParameterType.RequestBody);
			IRestResponse response = client.Execute(request);

			byte[] byteArray = Encoding.UTF8.GetBytes(response.Content);
			//byte[] byteArray = Encoding.ASCII.GetBytes(contents);
			MemoryStream stream = new MemoryStream(byteArray);

			HtmlDocument doc = new HtmlDocument();
			doc.Load(stream);
			//doc.LoadHtml(response.Content);

			var Lite_Result_Description = doc.DocumentNode.Descendants().Where(node => node.Name == "Lite_Result_Description");
			var nodes = doc.DocumentNode.SelectNodes("//form");
			var a = doc.DocumentNode.SelectNodes("//*[@name='Lite_Result_Description']");
			string awnser = "";

			foreach (var item in a)
			{
				foreach (var items in item.Attributes)
				{
					var b = items;

					if (items.Name == "value")
					{
						awnser = items.Value;
					}
				}

			}

			Payment payment = _context.Payment.Where(m => m.Lite_Merchant_Trace == update.Lite_Merchant_Trace).FirstOrDefault();


			if (payment != null)
			{
				payment.Lite_Result_Description = awnser;
				_context.Payment.Update(payment);
				var results = _context.SaveChanges();
			}


			Response.Redirect(url + update.Lite_Merchant_Trace.ToString());




			return true;
		}

	}
}