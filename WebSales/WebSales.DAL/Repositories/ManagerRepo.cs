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
    public class ManagerRepo : GenericRepository<Manager>, IManagerRepo
    {
        public ManagerRepo(DbContext context) : base(context)
        { }

        public async Task<IEnumerable<Manager>> GetManagersByFilter(ManagerFilterModel managerFilter)
        {
            if (managerFilter == null)
            {
                throw new ArgumentNullException("");
            }

            var managers = await GetAll();

            if (managerFilter.Name != null)
            {
                managers = managers.Where(x => x.Name == managerFilter.Name);
            }

            if (managerFilter.Age > 0)
            {
                managers = managers.Where(x => x.Age == managerFilter.Age);
            }

            return managers;
        }
    }
}
