using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebSales.DAL;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Models;
using WebSales.Models;
using WebSales.Models.Client;

namespace WebSales.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        public ViewResult Index()
        {
            return View();
        }

        public PartialViewResult Load(int? page)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Client, ClientIndexView>());
            var mapper = new Mapper(config);
            var clients = mapper.Map<List<ClientIndexView>>(unitOfWork.ClientRepo.GetAll());

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return PartialView(clients.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public JsonResult Create(ClientCreateView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ClientCreateView, Client>());
                    var mapper = new Mapper(config);
                    var client = mapper.Map<ClientCreateView, Client>(model);

                    unitOfWork.ClientRepo.Insert(client);
                    unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch
                {
                    return Json(new { result = false, message = "Server error, when try add client!" });
                }
            }

            return Json(new { result = false, message = ModelState.Select(x => x.Value.Errors).First() });
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
        public JsonResult Edit(ClientEditView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<ClientEditView, Client>());
                    var map = new Mapper(config);
                    var client = map.Map<ClientEditView, Client>(model);

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