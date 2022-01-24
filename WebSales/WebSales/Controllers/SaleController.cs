using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebSales.DAL;
using WebSales.DAL.Models;
using WebSales.Models;

namespace WebSales.Controllers
{
    public class SaleController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();
        public ViewResult Index()
        {
            return View();
        }

        public PartialViewResult Load(SaleFilter saleFilter)
        {
            var sales = unitOfWork.SaleRepo.GetAll();

            if (saleFilter.Client != null)
            {
                sales = sales.Where(x => x.Client.Name == saleFilter.Client).ToList();
            }

            if (saleFilter.Product != null)
            {
                sales = sales.Where(x => x.Product.Name == saleFilter.Product);
            }

            if (saleFilter.Manager != null)
            {
                sales = sales.Where(x => x.Manager.Name == saleFilter.Manager);
            }

            if (saleFilter.Sum > 0)
            {
                sales = sales.Where(x => x.Sum == saleFilter.Sum);
            }

            var config = new MapperConfiguration(cfg => cfg.CreateMap<Sale, SaleIndexView>()
            .ForMember("Client", opt => opt.MapFrom(c => c.Client.Name))
            .ForMember("Product", opt => opt.MapFrom(c => c.Product.Name))
            .ForMember("Manager", opt => opt.MapFrom(c => c.Manager.Name)));
            var map = new Mapper(config);
            var salesView = map.Map<IEnumerable<Sale>, List<SaleIndexView>>(sales);

            ViewBag.Filter = new SaleFilter();

            return PartialView(salesView);
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult Create()
        {
            ViewBag.Clients = new SelectList(unitOfWork.ClientRepo.GetAll(), "ClientId", "Name");
            ViewBag.Products = new SelectList(unitOfWork.ProductRepo.GetAll(), "ProductId", "Name");
            ViewBag.Managers = new SelectList(unitOfWork.ManagerRepo.GetAll(), "ManagerId", "Name");

            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult Create(SaleCreateView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<SaleCreateView, Sale>());
                    var map = new Mapper(config);
                    var sale = map.Map<SaleCreateView, Sale>(model);

                    unitOfWork.SaleRepo.Insert(sale);
                    unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch (Exception ex)
                {
                    return Json(new { result = false, message = ex.Message });
                }
            }

            return Json(new { result = false, message = "Invalid model" });
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult Edit(int id)
        {
            if (id > 0)
            {
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<Sale, SaleEditView>());
                    var map = new Mapper(config);
                    var sale = map.Map<Sale, SaleEditView>(unitOfWork.SaleRepo.GetById(id));

                    ViewBag.Clients = new SelectList(unitOfWork.ClientRepo.GetAll(), "ClientId", "Name");
                    ViewBag.Products = new SelectList(unitOfWork.ProductRepo.GetAll(), "ProductId", "Name");
                    ViewBag.Managers = new SelectList(unitOfWork.ManagerRepo.GetAll(), "M`anagerId", "Name");

                    return PartialView(sale);
                }
                catch
                {
                    return PartialView("~/Views/Shared/Error.cshtml");
                }
            }

            return PartialView("~/Views/Shared/Error.cshtml");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public JsonResult Edit(SaleEditView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<SaleEditView, Sale>());
                    var map = new Mapper(config);
                    var sale = map.Map<SaleEditView, Sale>(model);

                    unitOfWork.SaleRepo.Update(sale);
                    unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch (Exception ex)
                {
                    return Json(new { result = false, message = ex.Message });
                }
            }

            return Json(new { result = false, message = "Invalid model" });
        }

        [Authorize(Roles = "Admin")]
        public JsonResult Delete(int id)
        {
            try
            {
                unitOfWork.SaleRepo.Delete(id);
                unitOfWork.Save();

                return Json(new { result = true });
            }
            catch (Exception ex)
            {
                return Json(new { result = false, ex.Message });
            }
        }
    }
}