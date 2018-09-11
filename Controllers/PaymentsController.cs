using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using RestSharp;
using Microsoft.AspNetCore.Rewrite;
using System.Web;
using HtmlAgilityPack;
using System.Text;
using System.IO;
using WebApiJwt.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using WebApiJwt.Services;
using System.Threading;

namespace WebApiJwt.Controllers
{
    public class Update
    {
        public string Status { get; set; }
        public int Lite_Merchant_Trace { get; set; }
        public string Lite_Result_Description { get; set; }
        public string Lite_Order_Amount { get; set; }
        public string Lite_Order_LineItems_Product_1 { get; set; }
        public string Lite_Order_LineItems_Quantity_1 { get; set; }
        public string Lite_Order_LineItems_Amount_1 { get; set; }
        public string Lite_ConsumerOrderID_PreFix { get; set; }
        public string Ecom_BillTo_Online_Email { get; set; }
        public string Ecom_Payment_Card_Protocols { get; set; }
        public string Ecom_ConsumerOrderID { get; set; }
        public string Ecom_TransactionComplete { get; set; }

    }

    [Produces("application/json")]
    [Route("api/Payments")]
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private static IHostingEnvironment _env;

        public PaymentsController(ApplicationDbContext context, IHostingEnvironment env)
        {
            _context = context;
            _env = env;

        }



        [HttpPost]
        public bool Success(Update update)
        {
            string url = "http://localhost:4200/#/order/success/?id=";

            if (_env.IsProduction())
            {
                url = "http://www.schoolmall.co.za/#/order/success/?id=";
            }
            else
            {
                url = "http://localhost:4200/#/order/success/?id=";
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

                    if (items.Name == "value") {
                        awnser = items.Value;
                    }
                }

            }

            Payment payment = _context.Payment.Where(m => m.Lite_Merchant_Trace == update.Lite_Merchant_Trace).FirstOrDefault();


            if (payment != null) {
                payment.Lite_Result_Description = awnser;
                _context.Payment.Update(payment);
                var results = _context.SaveChanges();

                #region SMS Auth
                string authorization = _context.SMSCridentials.Select(m => m.Auth).FirstOrDefault();
                authorization = "Basic " + authorization;
                var smAuth = new RestClient("https://rest.mymobileapi.com/v1/Authentication");
                var smsauthrequest = new RestRequest(Method.GET);
                smsauthrequest.AddHeader("cache-control", "no-cache");
                smsauthrequest.AddHeader("content-type", "application/json");
                smsauthrequest.AddHeader("authorization", authorization);
                IRestResponse smsauthresponse = smAuth.Execute(smsauthrequest);
                SmsAuthObject smsAuth = JsonConvert.DeserializeObject<SmsAuthObject>(smsauthresponse.Content);
                #endregion

                #region SMS Clinet

                var clientNumber = _context.PaymentAddress.Where(m => m.Lite_Merchant_Trace == update.Lite_Merchant_Trace).Select(s => s.deliveryContactNumber).FirstOrDefault();
                var smsTxT = _context.SMS.Where(m => m.Template == "Order").Select(s => s.TextMessage).FirstOrDefault();
                smsTxT = smsTxT.Replace("@Order", update.Lite_Merchant_Trace.ToString());

                string rawSMSStruct = "{\r\n  \"Messages\": [\r\n    {\r\n      \"Content\": \"@msg\",\r\n      \"Destination\": \"@number\"\r\n    }\r\n  ]\r\n}";
                rawSMSStruct = rawSMSStruct.Replace("@msg", smsTxT);
                rawSMSStruct = rawSMSStruct.Replace("@number", clientNumber);
                
                var smsClient = new RestClient("https://rest.mymobileapi.com/v1/bulkmessages");
                var smsClientRequest = new RestRequest(Method.POST);
                smsClientRequest.AddHeader("content-type", "application/json");
                smsClientRequest.AddHeader("authorization", "Bearer " + smsAuth.token);
                smsClientRequest.AddParameter("application/json", rawSMSStruct, ParameterType.RequestBody);
                IRestResponse smsClientResponse = smsClient.Execute(smsClientRequest);

                //email
                var pAddress = _context.PaymentAddress.Where(m => m.Lite_Merchant_Trace == update.Lite_Merchant_Trace).FirstOrDefault();
                var pPayment = _context.Payment.Where(m => m.Lite_Merchant_Trace == update.Lite_Merchant_Trace).FirstOrDefault();
                string email = _context.EmailTemplates.Where(m => m.TemplateType == "Confirm").Select(s => s.Template).FirstOrDefault();
                email = email.Replace("{{name}}", pAddress.recipientName);
                email = email.Replace("{{order}}", update.Lite_Merchant_Trace.ToString());
                MailService mailService = new MailService("mail.eazibiz.co.za", "eazibkpl", "3Wnk7usY", "info@schoolmall.co.za", pPayment.UserID, email, "Schoolmall.co.za confirm ");
                Thread mgrThread = new Thread(new ThreadStart(mailService.SendMail));
                mgrThread.Start();

                #endregion





            }


            Response.Redirect(url + update.Lite_Merchant_Trace.ToString());






            return true;
        }











    }
    public static class HtmlNodeExtensions
    {
        public static IEnumerable<HtmlNode> GetElementsByName(this HtmlNode parent, string name)
        {
            return parent.Descendants().Where(node => node.Name == name);
        }

        public static IEnumerable<HtmlNode> GetElementsByTagName(this HtmlNode parent, string name)
        {
            return parent.Descendants(name);
        }
    }


    public class SmsAuthObject
    {
        public string token { get; set; }
        public string schema { get; set; }
        public string expiresInMinutes { get; set; }
    }

}