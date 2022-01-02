using Sales.Entities.Abstractions;
using Sales.Entities.Models;
using System.Data.Entity;

namespace Sales.DAL.Repositories
{
    public class SaleRepo : BaseRepo<Sale>, ISaleRepo
    {
        public SaleRepo (DbContext context) :base (context)
        { }
    }
}
