using System.Data.Entity;

namespace Sales.Entities.Abstractions
{
    public interface IEFContextFactory
    {
        DbContext GetContext();
    }
}
