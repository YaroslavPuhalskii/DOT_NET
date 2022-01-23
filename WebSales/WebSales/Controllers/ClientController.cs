using AutoMapper;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebSales.DAL;
using WebSales.DAL.Models;
using WebSales.Models;

namespace WebSales.Controllers
{
    public class ClientController : Controller
    {
        private readonly UnitOfWork unitOfWork = new UnitOfWork();

        public ViewResult Index()
        {
            return View();
        }

        public PartialViewResult Load()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientIndexView>());
            var mapper = new Mapper(config);
            var clients = mapper.Map<List<ClientIndexView>>(unitOfWork.ClientRepo.GetAll());

            return PartialView(clients);
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Create(ClientCreateView obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ClientCreateView, Client>());
                    var mapper = new Mapper(config);
                    var client = mapper.Map<ClientCreateView, Client>(obj);

                    unitOfWork.ClientRepo.Insert(client);
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
                    var conf = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientEditView>());
                    var map = new Mapper(conf);
                    var client = map.Map<Client, ClientEditView>(unitOfWork.ClientRepo.GetById(id));

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
        public JsonResult Edit(ClientEditView obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ClientEditView, Client>());
                    var map = new Mapper(config);
                    var client = map.Map<ClientEditView, Client>(obj);

                    unitOfWork.ClientRepo.Update(client);
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
                    unitOfWork.ClientRepo.Delete(id);
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