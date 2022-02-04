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
using WebSales.Models.Product;

namespace WebSales.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;

        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private const int _pageSize = 8;

        public ProductController()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Product, ProductIndexView>();
                cfg.CreateMap<ProductCreateView, Product>();
                cfg.CreateMap<Product, ProductEditView>();
                cfg.CreateMap<ProductEditView, Product>();
                cfg.CreateMap<ProductFilter, ProductFilterModel>();
            });

            _mapper = new Mapper(config);
        }

        public ViewResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> Load(ProductFilter productFilter, int? page)
        {
            var filter = _mapper.Map<ProductFilter, ProductFilterModel>(productFilter);

            var products = await unitOfWork.GetProductRepo.GetProductsByFilter(filter);

            var productView = _mapper.Map<IEnumerable<Product>, List<ProductIndexView>>(products);

            ViewBag.Filter = new ProductFilter();

            int pageNumber = (page ?? 1);
            return PartialView(productView.ToPagedList(pageNumber, _pageSize));
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Create(ProductCreateView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = _mapper.Map<ProductCreateView, Product>(model);

                    unitOfWork.GetProductRepo.Insert(client);
                    await unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"{DateTime.Now.ToLongTimeString()} : Server error, when try to add a product!");
                    return Json(new { result = false, message = "Server error, when try add client!" });
                }
            }

            _logger.Error($"{DateTime.Now.ToLongTimeString()} : Invalid model {nameof(model)}! {ModelState.Select(x => x.Value.Errors).First().First().ErrorMessage}");
            return Json(new { result = false, message = "Invalid model!" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<PartialViewResult> Edit(int id)
        {
            if (id > 0)
            {
                try
                {
                    var client = _mapper.Map<Product, ProductEditView>(await unitOfWork.GetProductRepo.GetById(id));

                    return PartialView(client);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"{DateTime.Now.ToLongTimeString()} : Server error, when trying to get product by Id : {id} !");
                    return PartialView("~/Views/Shared/Error.cshtml");
                }
            }

            _logger.Error($"{DateTime.Now.ToLongTimeString()} : Id less 1!");
            return PartialView("~/Views/Shared/Error.cshtml");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Edit(ProductEditView model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = _mapper.Map<ProductEditView, Product>(model);

                    unitOfWork.GetProductRepo.Update(client);
                    await unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"{DateTime.Now.ToLongTimeString()} : Server error, when trying to edit a product!");
                    return Json(new { result = false, message = ex.Message });
                }
            }

            _logger.Error($"{DateTime.Now.ToLongDateString()} : Invalid model {nameof(model)}! {ModelState.Select(x => x.Value.Errors).First().First().ErrorMessage}");
            return Json(new { result = false, message = "Model is invalid" });
        }

        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Delete(int id)
        {
            if (id > 0)
            {
                try
                {
                    await unitOfWork.GetProductRepo.Delete(id);
                    await unitOfWork.Save();

                    return Json(new { result = true });
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, $"{DateTime.Now.ToLongTimeString()} : Server error, when trying to delete a product!");
                    return Json(new { result = true, message = ex.Message });
                }
            }

            _logger.Error($"{DateTime.Now.ToLongTimeString()} : Id less 1!");
            return Json(new { result = false, message = "Id less 1!" });
        }
    }
}