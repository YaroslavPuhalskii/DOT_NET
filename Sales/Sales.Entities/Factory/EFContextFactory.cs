using Sales.Entities.Abstractions;
using System.Data.Entity;

namespace Sales.Entities.Factory
{
    public class EFContextFactory : IEFContextFactory
    {
        public DbContext GetContext()
        {
            return new EFContext();
        }
    }
}
