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
    public class SupplierController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SupplierController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpGet]
        public SuppliersViewModel SuppliersDetails(string id)
        {

            var supplier = (from a in _context.Suppliers
                            where a.SuppliersId == id
                            select new SuppliersViewModel()
                            {
                                ContactNumber = a.ContactNumber,
                                Email = a.Email,
                                FaxNumber = a.FaxNumber,
                                Name = a.Name,
                                SuppliersId = a.SuppliersId,
                                UserID = a.UserID,
                                Website = a.Website

                            }).FirstOrDefault();

            return supplier;
        }

        [Authorize]
        [HttpPost]
        public bool SuppliersUpdate([FromBody] SuppliersViewModel model)
        {
            Suppliers db = _context.Suppliers.Where(m => m.SuppliersId == model.SuppliersId).FirstOrDefault();


            if (db.SuppliersId == model.SuppliersId)
            {
                Mapper.Map(model, db);
                _context.Suppliers.Attach(db);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

            // 

            
        }

        [Authorize]
        [HttpPost]
        public bool SuppliersAdd([FromBody] SuppliersViewModel model)
        {
            Suppliers db = new Suppliers();

                Mapper.Map(model, db);
                _context.Suppliers.Add(db);
                _context.SaveChanges();
                return true;
        

            // 


        }


        [Authorize]
        [HttpGet]
        public List<SuppliersViewModel> Suppliers()
        {

            var suppliers = from a in _context.Suppliers
                      
                        select new SuppliersViewModel()
                        {
                            ContactNumber = a.ContactNumber,
                            Email = a.Email,
                            FaxNumber = a.FaxNumber,
                            Name = a.Name,
                            SuppliersId  = a.SuppliersId,
                            UserID = a.UserID,
                            Website = a.Website
                            
                        };

            return suppliers.ToList();
        }


        [Authorize]
        [HttpGet]
        public SupplierProducts SuppliersProductsImage(string SuppliersId, int ProductID)
        {
            return _context.SupplierProducts.Where(m => m.SuppliersId == SuppliersId && m.ProductId == ProductID).FirstOrDefault();
        }
        

        [Authorize]
        [HttpGet]
        public List<SupplierProductsViewModel> SuppliersProducts(string id)
        {

            var suppliers = from a in _context.SupplierProducts
                            where a.SuppliersId == id
                            select new SupplierProductsViewModel()
                            {
                                BrandCode = a.BrandCode,
                                SuppliersId = a.SuppliersId,
                                CatalogueCode = a.CatalogueCode,
                                CategoryCode = a.CategoryCode,
                                Description = a.Description,
                               // Image = a.Image,

                               

                                ProductCode = a.ProductCode,
                                ProductId = a.ProductId,
                                RetailPrice = a.RetailPrice,
                                SupplierPrice = a.SupplierPrice,
                                UOMCode = a.UOMCode,
                                UserID = a.UserID,
                                HasImage =a.Image == null ? false:true,
                                Colour = a.Color,
								DescriptionAfrikaans = a.DescriptionAfrikaans,
								DescriptionDuel = a.DescriptionDuel
							
                                
                               
                            };


            

            return suppliers.ToList();
        }


        [Authorize]
        [HttpPost]
        public bool UpdateSuppliersProducts([FromBody] SupplierProductsViewModel model)
        {

           

                SupplierProducts db = _context.SupplierProducts.Where(c => c.ProductCode == model.ProductCode.ToString() && c.Color == model.Colour && c.UOMCode == model.UOMCode).FirstOrDefault();

                try
                {
                if (db != null)
                {

                    Mapper.Map(model, db);
                    
                    if (model.Image == null)
                    {
                        db.Image = null;
                    }

                    _context.SupplierProducts.Update(db);
                    var result = _context.SaveChanges();
                    return true;
                }
                else {
                    db = new SupplierProducts();
                    
                    Mapper.Map(model, db);
                    db.ProductId = 0;
                    db.Type008 = "008";
                    db.Type009 = "009";
                    db.Type010 = "010";
                    db.Type011 = "011";
                    db.Color = model.Colour;
                    db.SuppliersId = "1";

                    if (model.Image == null)
                    {
                        db.Image = null;
                    }

                    _context.SupplierProducts.Add(db);
                    var result = _context.SaveChanges();
                    return true;

                }
                
                }
                catch (Exception ex)
                {
                var error = ex.InnerException;
                return false;
                }
            
          
        }


        [Authorize]
        [HttpPost]
        public bool AddSuppliersProducts([FromBody] SupplierProductsViewModel model)
        {

            SupplierProducts db = new SupplierProducts();
            try
            {
                
                    Mapper.Map(model, db);
                    db.SuppliersId = "1";
                db.Type008 = "008";
                db.Type009 = "009";
                db.Type010 = "010";
                db.Type011 = "011";
                db.Color = model.Colour;
                    _context.SupplierProducts.Add(db);
                
                    var result = _context.SaveChanges();

                    return true;
                
            }
            catch (Exception ex)
            {
                var error = ex.InnerException;
                return false;
            }
        }


    }
}