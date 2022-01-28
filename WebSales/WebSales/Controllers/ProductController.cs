using AutoMapper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebSales.DAL;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Models;
using WebSales.Models.Product;

namespace WebSales.Controllers
{
    public class ProductController : Controller
    {
        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        private readonly IMapper _mapper;

        private const int _pageSize = 3;

        public ProductController()
        {
            var config = new MapperConfiguration(cfg => 
            {
                cfg.CreateMap<Product, ProductIndexView>();
                cfg.CreateMap<ProductCreateView, Product>();
                cfg.CreateMap<Product, ProductEditView>();
                cfg.CreateMap<ProductEditView, Product>();
            });

            _mapper = new Mapper(config);
        }

        public ViewResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> Load(int? page)
        {
            var products = _mapper.Map<List<ProductIndexView>>(await unitOfWork.ProductRepo.GetAll());

            int pageNumber = (page ?? 1);
            return PartialView(products.ToPagedList(pageNumber, _pageSize));
        }

        [HttpGet]
        public PartialViewResult Create()
        {
            return PartialView();
        }

        [Authorize(Roles = "Admin")]
        public async Task<JsonResult> Create(ProductCreateView obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = _mapper.Map<ProductCreateView, Product>(obj);

                    unitOfWork.ProductRepo.Insert(client);
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
                    var client = _mapper.Map<Product, ProductEditView>(await unitOfWork.ProductRepo.GetById(id));

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
        public async Task<JsonResult> Edit(ProductEditView obj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var client = _mapper.Map<ProductEditView, Product>(obj);

                    unitOfWork.ProductRepo.Update(client);
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
                    await unitOfWork.ProductRepo.Delete(id);
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