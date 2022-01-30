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
using WebSales.DAL.Filters;
using WebSales.DAL.Models;
using WebSales.Models.Sale;

namespace WebSales.Controllers
{
    public class SaleController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private const int _pageSize = 8;

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
                cfg.CreateMap<SaleFilter, SaleFilterModel>();
            });

            _mapper = new Mapper(config);
        }

        public ViewResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> Load(SaleFilter saleFilter, int? page)
        {
            var sale = _mapper.Map<SaleFilter, SaleFilterModel>(saleFilter);

            var sales = await unitOfWork.GetSaleRepo.GetSalesByFilter(sale);

            var salesView = _mapper.Map<IEnumerable<Sale>, List<SaleIndexView>>(sales);

            ViewBag.Filter = new SaleFilter();

            int pageNumber = (page ?? 1);
            return PartialView(salesView.ToPagedList(pageNumber, _pageSize));
        }

        [Authorize(Roles = "Admin")]
        public async Task<PartialViewResult> Create()
        {
            var salesList = new SalesListViewModel()
            {
                Clients = new SelectList(await unitOfWork.GetClientRepo.GetAll(), "Id", "Name"),
                Products = new SelectList(await unitOfWork.GetProductRepo.GetAll(), "Id", "Name"),
                Managers = new SelectList(await unitOfWork.GetManagerRepo.GetAll(), "Id", "Name")
            };

            return PartialView(salesList);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Create(SaleCreateView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var sale = _mapper.Map<SaleCreateView, Sale>(model);

                    unitOfWork.GetSaleRepo.Insert(sale);
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
                    var sale = _mapper.Map<Sale, SaleEditView>(await unitOfWork.GetSaleRepo.GetById(id));

                    var saleList = new SalesListViewModel()
                    {
                        ManagerId = sale.ManagerId,
                        ClientId = sale.ClientId,
                        ProductId = sale.ProductId,
                        Date = sale.Date,
                        Sum  = sale.Sum,
                        Clients = new SelectList(await unitOfWork.GetClientRepo.GetAll(), "Id", "Name"),
                        Products = new SelectList(await unitOfWork.GetProductRepo.GetAll(), "Id", "Name"),
                        Managers = new SelectList(await unitOfWork.GetManagerRepo.GetAll(), "Id", "Name")
                    };

                    return PartialView(saleList);
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

                    unitOfWork.GetSaleRepo.Update(sale);
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
                    await unitOfWork.GetSaleRepo.Delete(id);
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