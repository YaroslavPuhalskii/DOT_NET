using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebSales.DAL;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Models;
using WebSales.Models;

namespace WebSales.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        public ViewResult Index()
        {
            return View();
        }

        public PartialViewResult Load(int? page)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductIndexView>());
            var mapper = new Mapper(config);
            var products = mapper.Map<List<ProductIndexView>>(unitOfWork.ProductRepo.GetAll());

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return PartialView(products.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        public JsonResult Create(ProductCreateView obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductCreateView, Product>());
                    var mapper = new Mapper(config);
                    var client = mapper.Map<ProductCreateView, Product>(obj);

                    unitOfWork.ProductRepo.Insert(client);
                    unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch
                {
                    return Json(new { result = false, message = "Server error, when try add client!" });
                }
            }

            return Json(new { result = false, message = "Invalid model!" });
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult Edit(int id)
        {
            if (id > 0)
            {
                try
                {
                    var conf = new MapperConfiguration(cfg => cfg.CreateMap<Product, ProductEditView>());
                    var map = new Mapper(conf);
                    var client = map.Map<Product, ProductEditView>(unitOfWork.ProductRepo.GetById(id));

                    return PartialView(client);
                }
                catch
                {
                    return PartialView("~/Views/Shared/Error.cshtml");
                }
            }

            return PartialView("~/Views/Shared/Error.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Edit(ProductEditView obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ProductEditView, Product>());
                    var map = new Mapper(config);
                    var client = map.Map<ProductEditView, Product>(obj);

                    unitOfWork.ProductRepo.Update(client);
                    unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch (Exception ex)
                {
                    return Json(new { result = false, message = ex.Message });
                }
            }

            return Json(new { result = false, message = "Model is invalid" });
        }

        [Authorize(Roles = "Admin")]
        public JsonResult Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    unitOfWork.ProductRepo.Delete(id);
                    unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch (Exception ex)
                {
                    return Json(new { result = true, message = ex.Message });
                }
            }

            return Json(new { result = false, message = "Id less 0!" });
        }
    }
}