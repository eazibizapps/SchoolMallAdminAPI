﻿using System;
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
                                UserID = a.UserID

                            };

            return suppliers.ToList();
        }


        [Authorize]
        [HttpPost]
        public bool UpdateSuppliersProducts([FromBody] SupplierProductsViewModel model)
        {

           

                SupplierProducts db = _context.SupplierProducts.Where(c => c.ProductCode == model.ProductCode.ToString()).FirstOrDefault();

                try
                {
                    if (db.ProductCode == model.ProductCode.ToString())
                    {

                        Mapper.Map(model, db);
                        _context.SupplierProducts.Update(db);
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
        public bool AddSuppliersProducts([FromBody] SupplierProductsViewModel model)
        {

            SupplierProducts db = new SupplierProducts();
            try
            {
                
                    Mapper.Map(model, db);
                    db.SuppliersId = "1";
                    _context.SupplierProducts.Add(db);
                
                    var result = _context.SaveChanges();

                    return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}