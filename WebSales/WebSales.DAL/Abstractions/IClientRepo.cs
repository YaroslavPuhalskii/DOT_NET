using System.Collections.Generic;
using System.Threading.Tasks;
using WebSales.DAL.Filters;
using WebSales.DAL.Models;

namespace WebSales.DAL.Abstractions
{
    public interface IClientRepo : IGenericRepository<Client>
    {
        Task<IEnumerable<Client>> GetClientsByFilter(ClientFilterModel clientFilter);
    }
}
