using Sales.Entities.Abstractions;
using Sales.Entities.Models;
using System.Data.Entity;

namespace Sales.DAL.Repositories
{
    public class ProductRepo : BaseRepo<Product>, IProductRepo
    {
        public ProductRepo(DbContext context) : base(context)
        { }
    }
}
