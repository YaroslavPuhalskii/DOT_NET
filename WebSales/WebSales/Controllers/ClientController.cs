using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebSales.DAL;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Models;
using WebSales.Models.Client;

namespace WebSales.Controllers
{
    public class ClientController : Controller
    {
        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        private readonly IMapper _mapper;

        private const int _pageSize = 3;

        public ClientController()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Client, ClientIndexView>();
                cfg.CreateMap<ClientCreateView, Client>();
                cfg.CreateMap<Client, ClientEditView>();
                cfg.CreateMap<ClientEditView, Client>();
            });

            _mapper = new Mapper(config);
        }

        public ViewResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> Load(int? page)
        {
            var clients = _mapper.Map<List<ClientIndexView>>(await unitOfWork.ClientRepo.GetAll());

            int pageNumber = (page ?? 1);
            return PartialView(clients.ToPagedList(pageNumber, _pageSize));
        }

        [Authorize(Roles = "Admin")]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Create(ClientCreateView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = _mapper.Map<ClientCreateView, Client>(model);

                    unitOfWork.ClientRepo.Insert(client);
                    await unitOfWork.Save();

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
        public async Task<PartialViewResult> Edit(int id)
        {
            if (id > 0)
            {
                try
                {
                    var client = _mapper.Map<Client, ClientEditView>(await unitOfWork.ClientRepo.GetById(id));

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
        public async Task<JsonResult> Edit(ClientEditView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = _mapper.Map<ClientEditView, Client>(model);

                    unitOfWork.ClientRepo.Update(client);
                    await unitOfWork.Save();

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
        public async Task<JsonResult> Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    await unitOfWork.ClientRepo.Delete(id);
                    await unitOfWork.Save();

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