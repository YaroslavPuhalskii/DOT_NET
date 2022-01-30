using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Filters;
using WebSales.DAL.Models;

namespace WebSales.DAL.Repositories
{
    public class ClientRepo : GenericRepository<Client>, IClientRepo
    {
        public ClientRepo(DbContext context) : base(context)
        { }

        public async Task<IEnumerable<Client>> GetClientsByFilter(ClientFilterModel clientFilter)
        {
            if (clientFilter == null)
            {
                throw new ArgumentNullException("");
            }

            var clients = await GetAll();

            if (clientFilter.Name != null)
            {
                clients = clients.Where(x => x.Name == clientFilter.Name);
            }

            if (clientFilter.Age > 0)
            {
                clients = clients.Where(x => x.Age == clientFilter.Age);
            }

            return clients;
        }
    }
}
