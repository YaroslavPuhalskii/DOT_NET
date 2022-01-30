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
    public class ProductRepo : GenericRepository<Product>, IProductRepo
    {
        public ProductRepo(DbContext context) : base(context)
        { }

        public async Task<IEnumerable<Product>> GetProductsByFilter(ProductFilterModel productFilter)
        {
            if (productFilter == null)
            {
                throw new ArgumentNullException("");
            }

            var products = await GetAll();

            if (productFilter.Name != null)
            {
                products = products.Where(x => x.Name == productFilter.Name);
            }

            if (productFilter.Category != null)
            {
                products = products.Where(x => x.Category == productFilter.Category);
            }

            return products;
        }
    }
}
