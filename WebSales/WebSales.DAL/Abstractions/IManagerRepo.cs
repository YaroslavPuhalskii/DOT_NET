using System.Collections.Generic;
using System.Threading.Tasks;
using WebSales.DAL.Filters;
using WebSales.DAL.Models;

namespace WebSales.DAL.Abstractions
{
    public interface IManagerRepo : IGenericRepository<Manager>
    {
        IEnumerable<Manager> GetManagersByFilter(ManagerFilterModel managerFilter);
    }
}
