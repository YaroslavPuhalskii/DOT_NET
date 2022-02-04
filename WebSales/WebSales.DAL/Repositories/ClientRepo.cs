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
    public class ClientRepo : GenericRepository<Client>, IClientRepo
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ClientRepo(DbContext context) : base(context)
        { }

        public async Task<IEnumerable<Client>> GetClientsByFilter(ClientFilterModel clientFilter)
        {
            if (clientFilter == null)
            {
                _logger.Error($"{nameof(clientFilter)} can't be null!");
                throw new ArgumentNullException($"{nameof(clientFilter)} can't be null!");
            }

            var clients = GetDbSet;
            IQueryable<Client> client = null;
            if (clientFilter.Name != null)
            {
                client = clients.Where(x => x.Name == clientFilter.Name);
            }

            if (clientFilter.Age > 0)
            {
                client = (client ?? clients).Where(x => x.Age == clientFilter.Age);
            }

            return client.ToList();
        }
    }
}
