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
    public class ManagerRepo : GenericRepository<Manager>, IManagerRepo
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public ManagerRepo(DbContext context) : base(context)
        { }

        public IEnumerable<Manager> GetManagersByFilter(ManagerFilterModel managerFilter)
        {
            if (managerFilter == null)
            {
                _logger.Error($"{nameof(managerFilter)} can't be null!");
                throw new ArgumentNullException($"{nameof(managerFilter)} can't be null!");
            }

            var dbSet = GetDbSet;
            IQueryable<Manager> managers = null;

            if (managerFilter.Name != null)
            {
                managers = dbSet.Where(x => x.Name == managerFilter.Name);
            }

            if (managerFilter.Age > 0)
            {
                managers = (managers ?? dbSet).Where(x => x.Age == managerFilter.Age);
            }

            return managers ?? dbSet;
        }
    }
}
