using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebSales.DAL;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Models;
using WebSales.Models.Manager;

namespace WebSales.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        private readonly IMapper _mapper;

        private const int _pageSize = 3;

        public ManagerController()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Manager, ManagerIndexView>();
                cfg.CreateMap<ManagerCreateView, Manager>();
                cfg.CreateMap<Manager, ManagerEditView>();
                cfg.CreateMap<ManagerEditView, Manager>();
            });

            _mapper = new Mapper(config);
        }

        public ViewResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> Load(int? page)
        {
            var managers = _mapper.Map<List<ManagerIndexView>>(await unitOfWork.ManagerRepo.GetAll());

            int pageNumber = (page ?? 1);
            return PartialView(managers.ToPagedList(pageNumber, _pageSize));
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Create(ManagerCreateView obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var manager = _mapper.Map<ManagerCreateView, Manager>(obj);

                    unitOfWork.ManagerRepo.Insert(manager);
                    await unitOfWork.Save();

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
        public async Task<PartialViewResult> Edit(int id)
        {
            if (id > 0)
            {
                try
                {
                    var manager = _mapper.Map<Manager, ManagerEditView>(await unitOfWork.ManagerRepo.GetById(id));

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
        public async Task<JsonResult> Edit(ManagerEditView obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var manager = _mapper.Map<ManagerEditView, Manager>(obj);

                    unitOfWork.ManagerRepo.Update(manager);
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
                    await unitOfWork.ManagerRepo.Delete(id);
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