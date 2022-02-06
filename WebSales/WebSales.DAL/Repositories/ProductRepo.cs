using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Filters;
using WebSales.DAL.Models;

namespace WebSales.DAL.Repositories
{
    public class ProductRepo : GenericRepository<Product>, IProductRepo
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ProductRepo(DbContext context) : base(context)
        { }

        public IEnumerable<Product> GetProductsByFilter(ProductFilterModel productFilter)
        {
            if (productFilter == null)
            {
                _logger.Error($"{nameof(productFilter)} can't be null!");
                throw new ArgumentNullException($"{nameof(productFilter)} can't be null!");
            }

            var dbSet = GetDbSet;
            IQueryable<Product> products = null;

            if (productFilter.Name != null)
            {
                products = dbSet.Where(x => x.Name == productFilter.Name);
            }

            if (productFilter.Category != null)
            {
                products = (products ?? dbSet).Where(x => x.Category == productFilter.Category);
            }

            return (products ?? dbSet);
        }
    }
}
