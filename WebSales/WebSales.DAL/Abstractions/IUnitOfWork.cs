using System;
using System.Threading.Tasks;
using WebSales.DAL.Models;

namespace WebSales.DAL.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        IClientRepo GetClientRepo { get; }

        IManagerRepo GetManagerRepo { get; }

        IProductRepo GetProductRepo { get; }

        ISaleRepo GetSaleRepo { get; }

        Task Save();
    }
}
