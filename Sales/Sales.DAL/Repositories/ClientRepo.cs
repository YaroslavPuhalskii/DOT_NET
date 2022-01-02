using Sales.Entities.Abstractions;
using Sales.Entities.Models;
using System.Data.Entity;

namespace Sales.DAL.Repositories
{
    public class ClientRepo : BaseRepo<Client>, IClientRepo
    {
        public ClientRepo(DbContext context) : base(context)
        { }
    }
}
