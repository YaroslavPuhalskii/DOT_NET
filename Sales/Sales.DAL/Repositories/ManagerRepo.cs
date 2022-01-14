using Sales.Entities.Models;
using System.Data.Entity;

namespace Sales.DAL.Repositories
{
    public class ManagerRepo : BaseRepo<Manager>, IManagerRepo
    {
        public ManagerRepo(DbContext context) : base(context)
        { }
    }
}
