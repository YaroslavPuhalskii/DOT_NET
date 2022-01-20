using System;
using WebSales.DAL.Models;

namespace WebSales.DAL.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Client> ClientRepo { get; }

        IGenericRepository<Manager> ManagerRepo { get; }

        IGenericRepository<Product> ProductRepo { get; }

        IGenericRepository<Sale> SaleRepo { get; }

        void Save();
    }
}
