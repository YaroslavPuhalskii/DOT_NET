using System;
using System.Threading.Tasks;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Models;
using WebSales.DAL.Repositories;

namespace WebSales.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _context = new EFContext();

        private bool disposedValue;

        private IGenericRepository<Client> _clientRepo;

        private IGenericRepository<Manager> _managerRepo;

        private IGenericRepository<Product> _productRepo;

        private ISaleRepo _saleRepo;

        public IGenericRepository<Client> ClientRepo => _clientRepo = _clientRepo ?? new GenericRepository<Client>(_context);

        public IGenericRepository<Manager> ManagerRepo => _managerRepo = _managerRepo ?? new GenericRepository<Manager>(_context);

        public IGenericRepository<Product> ProductRepo => _productRepo = _productRepo ?? new GenericRepository<Product>(_context);

        public ISaleRepo GetSaleRepo => _saleRepo = _saleRepo ?? new SaleRepo(_context);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
