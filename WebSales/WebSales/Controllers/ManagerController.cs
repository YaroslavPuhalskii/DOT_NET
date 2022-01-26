using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebSales.DAL;
using WebSales.DAL.Models;
using WebSales.Models;

namespace WebSales.Controllers
{
    public class ManagerController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public ViewResult Index()
        {
            return View();
        }

        public PartialViewResult Load(int? page)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Manager, ManagerIndexView>());
            var mapper = new Mapper(config);
            var managers = mapper.Map<List<ManagerIndexView>>(unitOfWork.ManagerRepo.GetAll());
            
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return PartialView(managers.ToPagedList(pageNumber, pageSize));
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        public JsonResult Create(ManagerCreateView obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ManagerCreateView, Manager>());
                    var mapper = new Mapper(config);
                    var manager = mapper.Map<ManagerCreateView, Manager>(obj);

                    unitOfWork.ManagerRepo.Insert(manager);
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
                    var conf = new MapperConfiguration(cfg => cfg.CreateMap<Manager, ManagerEditView>());
                    var map = new Mapper(conf);
                    var manager = map.Map<Manager, ManagerEditView>(unitOfWork.ManagerRepo.GetById(id));

                    return PartialView(manager);
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
        public JsonResult Edit(ManagerEditView obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ManagerEditView, Manager>());
                    var map = new Mapper(config);
                    var manager = map.Map<ManagerEditView, Manager>(obj);

                    unitOfWork.ManagerRepo.Update(manager);
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
                    unitOfWork.ManagerRepo.Delete(id);
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