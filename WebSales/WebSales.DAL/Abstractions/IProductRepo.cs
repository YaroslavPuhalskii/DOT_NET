using System.Collections.Generic;
using System.Threading.Tasks;
using WebSales.DAL.Filters;
using WebSales.DAL.Models;

namespace WebSales.DAL.Abstractions
{
    public interface IProductRepo : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByFilter(ProductFilterModel productFilter);
    }
}
