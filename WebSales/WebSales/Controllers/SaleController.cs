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
using WebSales.Models.Sale;

namespace WebSales.Controllers
{
    public class SaleController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private const int _pageSize = 3;

        public SaleController()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Sale, SaleIndexView>()
                .ForMember("Client", opt => opt.MapFrom(c => c.Client.Name))
                .ForMember("Product", opt => opt.MapFrom(c => c.Product.Name))
                .ForMember("Manager", opt => opt.MapFrom(c => c.Manager.Name));
                cfg.CreateMap<SaleCreateView, Sale>();
                cfg.CreateMap<Sale, SaleEditView>();
                cfg.CreateMap<SaleEditView, Sale>();
            });

            _mapper = new Mapper(config);
        }

        public ViewResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> Load(SaleFilter saleFilter, int? page)
        {
            var sales = await unitOfWork.SaleRepo.GetAll();

            if (saleFilter.Client != null)
            {
                sales = sales.Where(x => x.Client.Name == saleFilter.Client);
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

            var salesView = _mapper.Map<IEnumerable<Sale>, List<SaleIndexView>>(sales);

            ViewBag.Filter = new SaleFilter();

            int pageNumber = (page ?? 1);
            return PartialView(salesView.ToPagedList(pageNumber, _pageSize));
        }

        [Authorize(Roles = "Admin")]
        public async Task<PartialViewResult> Create()
        {
            ViewBag.Clients = new SelectList(await unitOfWork.ClientRepo.GetAll(), "Id", "Name");
            ViewBag.Products = new SelectList(await unitOfWork.ProductRepo.GetAll(), "Id", "Name");
            ViewBag.Managers = new SelectList(await unitOfWork.ManagerRepo.GetAll(), "Id", "Name");

            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> Create(SaleCreateView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sale = _mapper.Map<SaleCreateView, Sale>(model);

                    unitOfWork.SaleRepo.Insert(sale);
                    await unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch (Exception ex)
                {
                    _logger.Error($"{DateTime.Now.ToLongTimeString()} : Server error, when try to add a sale! {ex.Message}");
                    return Json(new { result = false, message = ex.Message });
                }
            }

            _logger.Error($"{DateTime.Now.ToLongTimeString()} : Invalid model {nameof(model)}! {ModelState.Select(x => x.Value.Errors).First()}");
            return Json(new { result = false, message = "Invalid model" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<PartialViewResult> Edit(int id)
        {
            if (id > 0)
            {
                try
                {
                    var sale = _mapper.Map<Sale, SaleEditView>(await unitOfWork.SaleRepo.GetById(id));

                    ViewBag.Clients = new SelectList(await unitOfWork.ClientRepo.GetAll(), "Id", "Name");
                    ViewBag.Products = new SelectList(await unitOfWork.ProductRepo.GetAll(), "Id", "Name");
                    ViewBag.Managers = new SelectList(await unitOfWork.ManagerRepo.GetAll(), "Id", "Name");

                    return PartialView(sale);
                }
                catch (Exception ex)
                {
                    _logger.Error($"{DateTime.Now.ToLongTimeString()} : Server error, when trying to get sale by Id! {ex.Message}");
                    return PartialView("~/Views/Shared/Error.cshtml");
                }
            }

            _logger.Error($"{DateTime.Now.ToLongTimeString()} : Id less 1!");
            return PartialView("~/Views/Shared/Error.cshtml");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<JsonResult> Edit(SaleEditView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sale = _mapper.Map<SaleEditView, Sale>(model);

                    unitOfWork.SaleRepo.Update(sale);
                    await unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch (Exception ex)
                {
                    _logger.Error($"{DateTime.Now.ToLongTimeString()} : Server error, when trying to edit a sale! {ex.Message}");
                    return Json(new { result = false, message = ex.Message });
                }
            }

            _logger.Error($"{DateTime.Now.ToLongDateString()} : Invalid model {nameof(model)}! {ModelState.Select(x => x.Value.Errors).First()}");
            return Json(new { result = false, message = "Invalid model" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    await unitOfWork.SaleRepo.Delete(id);
                    await unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch (Exception ex)
                {
                    _logger.Error($"{DateTime.Now.ToLongTimeString()} : Server error, when trying to delete a sale! {ex.Message}");
                    return Json(new { result = false, ex.Message });
                }
            }

            _logger.Error($"{DateTime.Now.ToLongTimeString()} : Id less 1!");
            return Json(new { result = false, message = "Id less 1!" });
        }
    }
}