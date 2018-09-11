using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Entities;
using WebApiJwt.ViewModels;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApiJwt.Models;
using System.Data;
using WebApiJwt.Services;
using System.Threading;

namespace WebApiJwt.Controllers
{
	[Route("[controller]/[action]")]
	public class OrdersController : Controller
    {
		private readonly ApplicationDbContext _context;

		public OrdersController(ApplicationDbContext context)
		{
			_context = context;
		}
		//[Authorize]
		[HttpPost]
		[Authorize]
		public List<OrdersViewModel> GetOrders() {

			List<OrdersViewModel> orders = new List<OrdersViewModel>();
			try
			{

				_context.Database.OpenConnection();

				using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
				{
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					cmd.CommandText = "sp_GetOrders";

					using (var reader = cmd.ExecuteReader())
					{
						orders = reader.MapToList<OrdersViewModel>();
					}

				}

				_context.Database.CloseConnection();
				return orders;

			}
			catch (Exception ex)
			{
                var error = ex.InnerException;
                return new List<OrdersViewModel>();
			}

		}

        [HttpPost]
        [Authorize]
        public List<Order_SummaryViewModel> GetOrdersSummary()
        {

            List<Order_SummaryViewModel> orders = new List<Order_SummaryViewModel>();
            try
            {

                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_OrdersSummary";

                    using (var reader = cmd.ExecuteReader())
                    {
                        orders = reader.MapToList<Order_SummaryViewModel>();
                    }

                }

                _context.Database.CloseConnection();
                return orders;

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new List<Order_SummaryViewModel>();
            }

        }



       

        [HttpGet]
        [Authorize]
        public OrderDetail GetOrdersDetailSupplier(int id)
        {

            List<OrderPayment> orderPayment = new List<OrderPayment>();
            List<OrderDetailViewModel> orderDetails = new List<OrderDetailViewModel>();
            List<OrderAddress> orderAddress = new List<OrderAddress>();
            OrderDetail orderDetail = new OrderDetail();

            try
            {

                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetOrderPayment";

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id},
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        orderPayment = reader.MapToList<OrderPayment>();
                    }

                }


                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetOrderDetails";

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id},
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        orderDetails = reader.MapToList<OrderDetailViewModel>();
                    }

                }


                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetOrderAddress";

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id},
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        orderAddress = reader.MapToList<OrderAddress>();
                    }

                }





                _context.Database.CloseConnection();

                orderDetail.OrderAddress = orderAddress;
                orderDetail.OrderDetails = orderDetails;
                orderDetail.OrderPayment = orderPayment;

                return orderDetail;

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new OrderDetail();
            }

        }



        [HttpGet]
        [Authorize]
        public OrderDetail GetOrdersDetail(int id)
        {

            List<OrderPayment> orderPayment = new List<OrderPayment>();
            List<OrderDetailViewModel> orderDetails = new List<OrderDetailViewModel>();
            List<OrderAddress> orderAddress = new List<OrderAddress>();
            OrderDetail orderDetail = new OrderDetail();

            try
            {

                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetOrderPayment";

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id},
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        orderPayment = reader.MapToList<OrderPayment>();
                    }

                }


                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetOrderDetails";

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id},
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        orderDetails = reader.MapToList<OrderDetailViewModel>();
                    }

                }


                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_GetOrderAddress";

                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id},
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        orderAddress = reader.MapToList<OrderAddress>();
                    }

                }





                _context.Database.CloseConnection();

                orderDetail.OrderAddress = orderAddress;
                orderDetail.OrderDetails = orderDetails;
                orderDetail.OrderPayment = orderPayment;

                return orderDetail;

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new OrderDetail();
            }

        }




        [HttpGet]
        [Authorize]
        public List<SupplierOrders> GetOrders_N()
        {

            List<SupplierOrders> orders = new List<SupplierOrders>();
            try
            {

                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SupplierNEwOrders";

                    using (var reader = cmd.ExecuteReader())
                    {
                        orders = reader.MapToList<SupplierOrders>();
                    }

                }

                _context.Database.CloseConnection();
                return orders;

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new List<SupplierOrders>();
            }

        }

        [HttpGet]
        [Authorize]
        public List<SupplierOrders> GetOrders_A()
        {

            List<SupplierOrders> orders = new List<SupplierOrders>();
            try
            {

                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SupplierAcknowledgeOrders";

                    using (var reader = cmd.ExecuteReader())
                    {
                        orders = reader.MapToList<SupplierOrders>();
                    }

                }

                _context.Database.CloseConnection();
                return orders;

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new List<SupplierOrders>();
            }

        }


        [HttpGet]
        [Authorize]
        public List<SupplierOrders> GetOrders_R()
        {

            List<SupplierOrders> orders = new List<SupplierOrders>();
            try
            {

                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SupplierReadyOrders";

                    using (var reader = cmd.ExecuteReader())
                    {
                        orders = reader.MapToList<SupplierOrders>();
                    }

                }

                _context.Database.CloseConnection();
                return orders;

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new List<SupplierOrders>();
            }

        }


        [HttpGet]
        [Authorize]
        public List<SupplierOrders> GetOrders_C()
        {

            List<SupplierOrders> orders = new List<SupplierOrders>();
            try
            {

                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_SupplierCollectedOrders";

                    using (var reader = cmd.ExecuteReader())
                    {
                        orders = reader.MapToList<SupplierOrders>();
                    }

                }

                _context.Database.CloseConnection();
                return orders;

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new List<SupplierOrders>();
            }

        }


        //OrderStatuss
        [HttpGet]
        [Authorize]
        public List<OrderStatuss> OrderStatus(int id)
        {

            List<OrderStatuss> orders = new List<OrderStatuss>();
            try
            {

                _context.Database.OpenConnection();

                using (DbCommand cmd = _context.Database.GetDbConnection().CreateCommand())
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "sp_OrderStatus";
                    List<SqlParameter> sp = new List<SqlParameter>()
                    {
                        new SqlParameter() {ParameterName = "@OrderID", SqlDbType = SqlDbType.Int, Value = id},
                    };

                    cmd.Parameters.AddRange(sp.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        orders = reader.MapToList<OrderStatuss>();
                    }

                }

                _context.Database.CloseConnection();
                return orders;

            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return new List<OrderStatuss>();
            }


        }


        [HttpPost]
        [Authorize]
        public bool UpdateOrder([FromBody] OrderPayment model) {

            try
            {
                
                Payment payment = _context.Payment.Where(m => m.Lite_Merchant_Trace.ToString() == model.OrderNumber).FirstOrDefault();

                if (payment != null)
                {
                    payment.Lite_Result_Description = model.PaymentStatus;
                    _context.Payment.Update(payment);
                    _context.SaveChanges();
                    return true;
                }
                else {
                    return false;
                }

                

            }
            catch (Exception ex) {
                var err = ex.InnerException;
                return false;
            }

            
        }

        [HttpPost]
        [Authorize]
        public bool UpdateOrderStatus([FromBody] UpdateOrderStatus model)
        {

            try
            {

                Payment payment = _context.Payment.Where(m => m.Lite_Merchant_Trace == model.OrderNumber).FirstOrDefault();

                if (payment != null)
                {

                    if (model.OrderStatus == "C") {
                        //Send Mail
                        var pAddress = _context.PaymentAddress.Where(m => m.Lite_Merchant_Trace == model.OrderNumber).FirstOrDefault();
                        var pPayment = _context.Payment.Where(m => m.Lite_Merchant_Trace == model.OrderNumber).FirstOrDefault();
                        string email = _context.EmailTemplates.Where(m => m.TemplateType == "Route").Select(s => s.Template).FirstOrDefault();
                        email = email.Replace("{{name}}", pAddress.recipientName);
                        email = email.Replace("{{order}}", model.OrderNumber.ToString());

                        MailService mailService = new MailService("mail.eazibiz.co.za", "eazibkpl", "3Wnk7usY", "info@schoolmall.co.za", pPayment.UserID, email, "Schoolmall.co.za confirm ");
                        Thread mgrThread = new Thread(new ThreadStart(mailService.SendMail));
                        mgrThread.Start();



                    }

                    payment.OrderStatus = model.OrderStatus;
                    payment.UpdatedBy = model.UpdatedBy;
                    payment.UpdatedDate = model.UpdatedDate;
                    _context.Payment.Update(payment);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }



            }
            catch (Exception ex)
            {
                var err = ex.InnerException;
                return false;
            }


        }


    }
}