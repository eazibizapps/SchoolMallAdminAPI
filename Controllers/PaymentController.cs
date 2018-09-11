using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using WebApiJwt.Models;
using WebApiJwt.Entities;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace WebApiJwt.Controllers
{
	[Route("[controller]/[action]")]
	public class PaymentController : Controller
	{
		private readonly ApplicationDbContext _context;

		public PaymentController(ApplicationDbContext context )
		{
			_context = context;
		}

		[HttpPost]
		public bool UpdatePaymentType([FromBody]  PaymentUpdate model)
		{
			var payment = _context.Payment.Where(m => m.Lite_Merchant_Trace == model.Lite_Merchant_Trace).FirstOrDefault();

			if (payment != null)
			{
				payment.PaymentType = model.PaymentType;
				_context.Payment.Attach(payment);
				var result = _context.SaveChanges();
				return true;
			}
			else {
				return false;
			}



		}



		[HttpGet]
		public bool Remove(int id)
		{
			Payment payment = _context.Payment.Where(m => m.Lite_Merchant_Trace == id).FirstOrDefault();
			PaymentAddress paymentAddress = _context.PaymentAddress.Where(m => m.Lite_Merchant_Trace == id).FirstOrDefault();

			if (payment != null)
			{
				_context.Payment.Remove(payment);
				var result = _context.SaveChanges();
			}


			if (paymentAddress != null)
			{
				_context.PaymentAddress.Remove(paymentAddress);
				var result = _context.SaveChanges();
			}


			List<PaymentLists> paymentLists = _context.PaymentLists.Where(m => m.Lite_Merchant_Trace == id).ToList();
			foreach (var listItem in paymentLists)
			{
				_context.PaymentLists.Remove(listItem);
				_context.SaveChanges();
			}

			if (paymentLists.Count > 0)
			{


				PaymentLists paylist = paymentLists.FirstOrDefault();
				List<PaymentProducts> paymentProducts = _context.PaymentProducts.Where(m => m.Lite_Merchant_Trace == id && m.PaymentListsId == paylist.PaymentListsId).ToList();
				foreach (var product in paymentProducts)
				{
					_context.PaymentProducts.Remove(product);
					_context.SaveChanges();
				}
			}



			return true;

		}

        public class Resultss {
            public bool awnser { get; set; }
            public string error { get; set; }
        }


		[HttpPost]
		public Resultss Success([FromBody]  PaymentData model) {
            Resultss resultss = new Resultss();
            string s = "";
            try
			{
             
                Payment payment = _context.Payment.Where(m => m.Lite_Merchant_Trace == model.PData.Lite_Merchant_Trace).FirstOrDefault();

				if (payment == null)
				{
					//Mapper.Map(model.PData, payment);
					_context.Payment.Add(model.PData);
					var result = _context.SaveChanges();
				}
				else
				{
					
					_context.Payment.Remove(payment);
					_context.SaveChanges();

					_context.Payment.Add(model.PData);
					var result = _context.SaveChanges();
				}


				PaymentAddress paymentAddress = _context.PaymentAddress.Where(m => m.Lite_Merchant_Trace == model.PData.Lite_Merchant_Trace).FirstOrDefault();

				if (paymentAddress == null)
				{
					model.AData.Lite_Merchant_Trace = model.PData.Lite_Merchant_Trace;
					_context.PaymentAddress.Add(model.AData);
					var result = _context.SaveChanges();
				}
				else {

					model.AData.Lite_Merchant_Trace = model.PData.Lite_Merchant_Trace;
					_context.PaymentAddress.Remove(paymentAddress);
					_context.SaveChanges();

					model.AData.Lite_Merchant_Trace = model.PData.Lite_Merchant_Trace;
					_context.PaymentAddress.Add(model.AData);
					_context.SaveChanges();

				
				}

				List<PaymentLists> paymentLists = _context.PaymentLists.Where(m => m.Lite_Merchant_Trace == model.PData.Lite_Merchant_Trace).ToList();
				foreach (var listItem in paymentLists)
				{
					_context.PaymentLists.Remove(listItem);
					_context.SaveChanges();
				}

				if (paymentLists.Count > 0) {


					PaymentLists paylist = paymentLists.FirstOrDefault();
					List<PaymentProducts> paymentProducts = _context.PaymentProducts.Where(m => m.Lite_Merchant_Trace == model.PData.Lite_Merchant_Trace && m.PaymentListsId == paylist.PaymentListsId).ToList();
					foreach (var product in paymentProducts)
					{
						_context.PaymentProducts.Remove(product);
						_context.SaveChanges();
					}
				}



                foreach (var item in model.OData)
                {

                    s = item.Total;

                    if (paymentLists.Count == 0)
                    {
                        PaymentLists paymentL = new PaymentLists { Grade = item.Grade, Lite_Merchant_Trace = model.PData.Lite_Merchant_Trace, NoOfLearners = item.NoOfLearners, Total = Convert.ToDecimal(item.Total) };
                        _context.PaymentLists.Add(paymentL);
                        var result = _context.SaveChanges();



                        List<PaymentProducts> Plist = new List<PaymentProducts>();

                        foreach (var items in item.Products)
                        {
                            PaymentProducts paymentProducts1 = new PaymentProducts() { Grade = items.Grade, Description = items.Description, LineTotal = items.LineTotal, Lite_Merchant_Trace = model.PData.Lite_Merchant_Trace, PaymentListsId = paymentL.PaymentListsId, ProductCode = items.ProductCode, Quantity = items.Quantity, ScoolGradesListID = items.ScoolGradesListID, Selected = items.Selected, UnitPrice = items.UnitPrice };
                            _context.PaymentProducts.Attach(paymentProducts1);
                        }

                        var results = _context.SaveChanges();

                    }


                }


                resultss.awnser = true;
                return resultss;

            }
			catch (Exception ex) {
				var aa = ex.InnerException;
                resultss.awnser = false;
                resultss.error = ex.Message.ToString();
                return resultss;
            }



			//var client = new RestClient("https://backoffice.nedsecure.co.za/Lite/Transactions/New/EasyAuthorise.aspx");
			//var request = new RestRequest(Method.POST);
			//request.AddHeader("postman-token", "f76737bf-5509-94d4-50e1-5ac40f6780d8");
			//request.AddHeader("cache-control", "no-cache");
			//request.AddHeader("content-type", "application/x-www-form-urlencoded");
			//request.AddParameter("application/x-www-form-urlencoded", "Lite_Merchant_Applicationid=%7B9EBD4A80-3CA5-4968-9004-1A5A827E506E%7D&Lite_Website_Fail_url=http%3A%2F%2Flocalhost%3A58128%2Fapi%2FFail&Lite_Website_TryLater_url=http%3A%2F%2Flocalhost%3A58128%2Fapi%2FTrylater&Lite_Website_Error_url=http%3A%2F%2Flocalhost%3A58128%2Fapi%2FTrylater&Lite_Order_LineItems_Product_1=Donation&Lite_Order_LineItems_Quantity_1=1&Lite_Order_LineItems_Amount_1=1000&Lite_ConsumerOrderID_PreFix=DML&Ecom_BillTo_Online_Email=mathhys.smith%40gmail.com&Ecom_Payment_Card_Protocols=iVeri&Ecom_ConsumerOrderID=AUTOGENERATE&Ecom_TransactionComplete=&Lite_Result_Description=&Lite_Merchant_Trace=779", ParameterType.RequestBody);
			//IRestResponse response = client.Execute(request);

		

		}

		[HttpGet]
		public int GetOrderNumber() {

			int ordervalue = 0;

			try
			{
				_context.Database.OpenConnection();
				using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "sp_GetNextOrder";
					
					using (var reader = cmd.ExecuteReader())
					{
						while (reader.Read()) {
							ordervalue = reader["OrderValue"] as int? ?? default(int);
						}
						
					}

				}
				_context.Database.CloseConnection();

				return ordervalue;

			}
			catch (Exception ex)
			{
                var error = ex.InnerException;
                return 0;
			}

		}


        public IActionResult Index()
        {
            return View();
        }
    }





	public class PaymentData {
		public Payment PData { get; set; }
		public ProductLists[] OData { get; set; }
		public PaymentAddress AData { get; set; }
		
	}







	
}