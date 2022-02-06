using System.Collections.Generic;
using System.Threading.Tasks;
using WebSales.DAL.Filters;
using WebSales.DAL.Models;

namespace WebSales.DAL.Abstractions
{
    public interface ISaleRepo : IGenericRepository<Sale>
    {
        IEnumerable<Sale> GetSalesByFilter(SaleFilterModel saleFilter);
    }
}
