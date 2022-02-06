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
    public class ClientRepo : GenericRepository<Client>, IClientRepo
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ClientRepo(DbContext context) : base(context)
        { }

        public IEnumerable<Client> GetClientsByFilter(ClientFilterModel clientFilter)
        {
            if (clientFilter == null)
            {
                _logger.Error($"{nameof(clientFilter)} can't be null!");
                throw new ArgumentNullException($"{nameof(clientFilter)} can't be null!");
            }

            var dbSet = GetDbSet;
            IQueryable<Client> clients = null;

            if (clientFilter.Name != null)
            {
                clients = dbSet.Where(x => x.Name == clientFilter.Name);
            }

            if (clientFilter.Age > 0)
            {
                clients = (clients ?? dbSet).Where(x => x.Age == clientFilter.Age);
            }

            return clients ?? dbSet;
        }
    }
}
