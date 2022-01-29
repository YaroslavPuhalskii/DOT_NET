using AutoMapper;
using NLog;
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
        private readonly IMapper _mapper;

        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

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
                catch (Exception ex)
                {
                    _logger.Error($"{DateTime.Now.ToLongTimeString()} : Server error, when try add client! {ex.Message}");
                    return Json(new { result = false, message = "Server error, when try add client!" });
                }
            }

            _logger.Error($"{DateTime.Now.ToLongTimeString()} : Invalid model {nameof(model)}! {ModelState.Select(x => x.Value.Errors).First()}");
            return Json(new { result = false, message = "Invalid model!" });
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
                catch (Exception ex)
                {
                    _logger.Error($"{DateTime.Now.ToLongDateString()} : Server error, when trying to get client by Id! {ex.Message}");
                    return PartialView("~/Views/Shared/Error.cshtml");
                }
            }

            _logger.Error($"{DateTime.Now.ToLongTimeString()} : Id less 1!");
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
                    _logger.Error($"{DateTime.Now.ToLongTimeString()} : Server error, when trying to edit a client! {ex.Message}");
                    return Json(new { result = false, message = ex.Message });
                }
            }

            _logger.Error($"{DateTime.Now.ToLongDateString()} : Invalid model {nameof(model)}! {ModelState.Select(x => x.Value.Errors).First()}");
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
                    _logger.Error($"{DateTime.Now.ToLongTimeString()} : Server error, when trying to delete a client! {ex.Message}");
                    return Json(new { result = true, message = ex.Message });
                }
            }

            _logger.Error($"{DateTime.Now.ToLongDateString()} : Id less 1!");
            return Json(new { result = false, message = "Id less 1!" });
        }
    }
}