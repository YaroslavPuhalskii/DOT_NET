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
        public SaleRepo(DbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Sale>> GetSalesByFilter(SaleFilterModel saleFilter)
        {
            if (saleFilter == null)
            {
                throw new ArgumentNullException("");
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
