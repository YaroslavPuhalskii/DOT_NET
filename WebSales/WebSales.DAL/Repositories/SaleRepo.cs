using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Filters;
using WebSales.DAL.Models;

namespace WebSales.DAL.Repositories
{
    public class SaleRepo : GenericRepository<Sale>, ISaleRepo
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public SaleRepo(DbContext context) : base(context)
        { }

        public async Task<IEnumerable<Sale>> GetSalesByFilter(SaleFilterModel saleFilter)
        {
            if (saleFilter == null)
            {
                _logger.Error($"{nameof(saleFilter)} can't be null!");
                throw new ArgumentNullException($"{nameof(saleFilter)} can't be null!");
            }

            var sales = await GetAll();

            if (saleFilter.Client != null)
            {
                sales =  sales.Where(x => x.Client.Name == saleFilter.Client);
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

            return sales;
        }
    }
}
