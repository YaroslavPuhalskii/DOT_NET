using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using WebSales.DAL;
using WebSales.DAL.Abstractions;
using WebSales.Models.Chart;

namespace WebSales.Controllers
{
    public class ChartsController : Controller
    {
        private readonly IUnitOfWork unitOfWork = new UnitOfWork();

        public ViewResult Index()
        {
            return View();
        }

        public async Task<PartialViewResult> ClientCharts()
        {
            var clients = await unitOfWork.GetClientRepo.GetAll();

            var group = clients.GroupBy(x => x.Age).Select(x => new { Age = x.Key, Count = x.Count() }).ToList();
            var clientModel = new ClientChartModel();

            group.ForEach(x => { clientModel.Ages.Add(x.Age); clientModel.Counts.Add(x.Count); });

            return PartialView(clientModel);
        }

        public async Task<PartialViewResult> ManagerCharts()
        {
            var managers = await unitOfWork.GetManagerRepo.GetAll();

            var group = managers.GroupBy(x => x.Age).Select(x => new { Age = x.Key, Count = x.Count() }).ToList();
            var managerModel = new ManagerChartModel();

            group.ForEach(x => { managerModel.Ages.Add(x.Age); managerModel.Counts.Add(x.Count); });

            return PartialView(managerModel);
        }

        public async Task<PartialViewResult> ProductCharts()
        {
            var products = await unitOfWork.GetProductRepo.GetAll();

            var group = products.GroupBy(x => x.Category).Select(x => new { Category = x.Key, Count = x.Count() }).ToList();
            var productModel = new ProductChartModel();

            group.ForEach(x => { productModel.Categories.Add(x.Category); productModel.Counts.Add(x.Count); });

            return PartialView(productModel);
        }

        public PartialViewResult BuildSaleChart()
        {
            return PartialView();
        }

        public async Task<PartialViewResult> SaleCharts()
        {
            var sales = await unitOfWork.GetSaleRepo.GetAll();

            var group = sales.GroupBy(x => x.Product.Name)
                .Select(x => new { Name = x.Key, Count = x.Count() })
                .OrderByDescending(x => x.Count).Take(3).ToList();
            var saleModel = new SaleChartModel();

            group.ForEach(x => { saleModel.Name.Add(x.Name); saleModel.Count.Add(x.Count); });

            return PartialView(saleModel);
        }
    }
}