using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApiJwt.Entities;
using Microsoft.AspNetCore.Authorization;
using WebApiJwt.ViewModels;
using AutoMapper;
using WebApiJwt.Models;

namespace WebApiJwt.Controllers
{
    [Route("[controller]/[action]")]
    public class SchoolProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolProductsController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize]
        [HttpGet]
        public List<SchoolProductsViewModel> Products()
        {
            var codes = from a in _context.SchoolProducts
                        orderby a.Active descending, a.Description ascending
                        select new SchoolProductsViewModel()
                        {
                            ProductId = a.ProductId,
                            Description = a.Description,
                            DescriptionAfrikaans = a.DescriptionAfrikaans,
                            DescriptionDual = a.DescriptionDual,
                            UserID = a.UserID,
                            Active = a.Active ??  default(bool),
                            CategoryCode = a.CategoryCode
                           
                            
                        }
                        
                        ;
            return codes.OrderBy(p => p.Description).Distinct().ToList();
        }


        [Authorize]
        [HttpPost]
        public List<SchoolProductsViewModel> ProductsGradeAddList([FromBody] List<ScoolGradesListViewModel> model)
        {
            
            if (model.Count() > 0)
            {
              var  codes = from a in _context.SchoolProducts
                           where !(from o in model
                                   select o.ProductId)
                                    .Contains(a.ProductId)
                           orderby a.Active descending, a.Description ascending
                            select new SchoolProductsViewModel()
                            {
                                ProductId = a.ProductId,
                                Description = a.Description,
                                DescriptionAfrikaans = a.DescriptionAfrikaans,
                                DescriptionDual = a.DescriptionDual,
                                UserID = a.UserID,
                                Active = a.Active ?? default(bool),
                                CategoryCode = a.CategoryCode
                            };


                return codes.OrderBy(p => p.Description).ToList();

            }
            else
            {
                var codes = from a in _context.SchoolProducts
                            orderby a.Active descending, a.Description ascending
                            select new SchoolProductsViewModel()
                            {
                                ProductId = a.ProductId,
                                Description = a.Description,
                                DescriptionAfrikaans = a.DescriptionAfrikaans,
                                DescriptionDual = a.DescriptionDual,
                                UserID = a.UserID,
                                Active = a.Active ?? default(bool),
                                CategoryCode = a.CategoryCode


                            };

                return codes.OrderBy(p => p.Description).ToList();

            }


            
        }



        [Authorize]
        [HttpPost]
        public bool AddProducts([FromBody] SchoolProductsViewModel model)
        {
            model.Active = true;
            
            SchoolProducts db = new SchoolProducts();
            try
            {
                Mapper.Map(model, db);
                db.Type009 = "009";
                _context.SchoolProducts.Add(db);
                var result = _context.SaveChanges();

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }


        [Authorize]
        [HttpPost]
        public bool EditProducts([FromBody] SchoolProductsViewModel model)
        {

            try
            {

                SchoolProducts db = _context.SchoolProducts.Where(p => p.ProductId == model.ProductId).FirstOrDefault();
                try
                {
                    if (db.ProductId == model.ProductId)
                    {
                        Mapper.Map(model, db);
                        db.Type009 = "009";
                        _context.SchoolProducts.Attach(db);
                        var result = _context.SaveChanges();

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }



        }


        [Authorize]
        [HttpPost]
        public bool AddProductLink([FromBody] SchoolProductsLinkViewModel model) {

            SchoolProductsLink db = new SchoolProductsLink();
            try {
                Mapper.Map(model, db);
                _context.SchoolProductsLink.Add(db);
                var result = _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
            
        }


        [Authorize]
        [HttpPost]
        public bool RemoveProductLink([FromBody] SchoolProductsLinkViewModel model)
        {

            SchoolProductsLink db = _context.SchoolProductsLink.Where(p => p.ProductId == model.ProductId && p.SuppliersId == model.SuppliersId).FirstOrDefault();
            try
            {
                if (db.ProductId == model.ProductId)
                {
                    Mapper.Map(model, db);
                    _context.SchoolProductsLink.Remove(db);
                    var result = _context.SaveChanges();
                    return true;
                }
                else {
                    return false;
                }

                

            }
            catch (Exception ex)
            {
                return false;
            }

        }



        [Authorize]
        [HttpGet]
        public List<SupplierProductsViewModel> GetSuppliersProduct(string id) {

            try
            {

                var ls = from a in _context.SupplierProducts
                         where a.SuppliersId == id
                         select new SupplierProductsViewModel()
                         {
                             ProductCode = a.ProductCode,
                             SuppliersId = a.SuppliersId,
                             BrandCode = a.BrandCode,
                             CatalogueCode = a.CatalogueCode,
                             CategoryCode = a.CategoryCode,
                             Description = a.Description,
                             //Image = a.Image,
                             ProductId = a.ProductId,
                             RetailPrice = a.RetailPrice,
                             SupplierPrice = a.SupplierPrice,
                             UOMCode = a.UOMCode,
                             UserID = a.UserID,
                             Colour = a.Color

                         };

                return ls.OrderBy(e => e.Description).ToList();
            }
            catch(Exception ex)
            {
                var a = ex.Message;
                return new List<SupplierProductsViewModel>();
                    
                    }

        }



        [Authorize]
        [HttpPost]
        public List<SupplierProductsViewModel> GetProductLink([FromBody] CodesViewModel model)
        {
           List<SchoolProductsLink> ls = _context.SchoolProductsLink.Where(m => m.ProductId.ToString() == model.Code).ToList();
            List<SupplierProductsViewModel> returnList = new List<SupplierProductsViewModel>();

            ls.ForEach( pl => {

                SupplierProducts sp = _context.SupplierProducts.Where(p => p.ProductCode == pl.ProductCode).FirstOrDefault();


                returnList.Add(new SupplierProductsViewModel {
                    BrandCode = sp.BrandCode,
                    ProductCode = sp.ProductCode,
                    CatalogueCode = sp.CatalogueCode,
                    CategoryCode = sp.CategoryCode,
                    Description = sp.Description,
                    Image = sp.Image,
                    ProductId = sp.ProductId,
                    RetailPrice = sp.RetailPrice,
                    SupplierPrice = sp.SupplierPrice,
                    SuppliersId = sp.SuppliersId,
                    UOMCode = sp.UOMCode,
                    UserID = sp.UserID,
                    Colour = sp.Color
                });
            });


            return returnList;

        }


        [Authorize]
        [HttpPost]
        public bool DeleteProducts([FromBody] SchoolProductsViewModel model)
        {

            SchoolProducts db = _context.SchoolProducts.Where(p => p.ProductId == model.ProductId).FirstOrDefault();
            try
            {
                if (db.ProductId == model.ProductId)
                {
                    Mapper.Map(model, db);
                    _context.SchoolProducts.Remove(db);
                    var result = _context.SaveChanges();

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }




    }

    
}