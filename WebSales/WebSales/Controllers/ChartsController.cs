using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSales.DAL;
using WebSales.DAL.Abstractions;

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
            var list = new List<int>();

            var ages = clients.Select(x => x.Age).Distinct().OrderBy(x => x).ToList();

            ages.ForEach(x => list.Add(clients.Count(t => t.Age == x)));

            ViewBag.AGES = ages;
            ViewBag.REP = list;

            return PartialView();
        }

        public async Task<PartialViewResult> ManagerCharts()
        {
            var managers = await unitOfWork.GetManagerRepo.GetAll();
            var list = new List<int>();

            var ages = managers.Select(x => x.Age).Distinct().OrderBy(x => x).ToList();

            ages.ForEach(x => list.Add(managers.Count(m => m.Age == x)));

            ViewBag.AGES = ages;
            ViewBag.REP = list;

            return PartialView();
        }

        public async Task<PartialViewResult> ProductCharts()
        {
            var products = await unitOfWork.GetProductRepo.GetAll();
            var list = new List<int>();

            var categories = products.Select(x => x.Category).Distinct().ToList();

            categories.ForEach(x => list.Add(products.Count(p => p.Category == x)));

            ViewBag.CATEGORIES = categories;
            ViewBag.REP = list;

            return PartialView();
        }
    }
}