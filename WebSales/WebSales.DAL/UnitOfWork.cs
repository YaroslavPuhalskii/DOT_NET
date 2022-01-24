using System;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Models;

namespace WebSales.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _context = new EFContext();

        private bool disposedValue;

        private IGenericRepository<Client> _clientRepo;

        private IGenericRepository<Manager> _managerRepo;

        private IGenericRepository<Product> _productRepo;

        private IGenericRepository<Sale> _saleRepo;

        public IGenericRepository<Client> ClientRepo => _clientRepo = _clientRepo ?? new GenericRepository<Client>(_context);

        public IGenericRepository<Manager> ManagerRepo => _managerRepo = _managerRepo ?? new GenericRepository<Manager>(_context);

        public IGenericRepository<Product> ProductRepo => _productRepo = _productRepo ?? new GenericRepository<Product>(_context);

        public IGenericRepository<Sale> SaleRepo => _saleRepo = _saleRepo ?? new GenericRepository<Sale>(_context);

        public void Save()
        {
            _context.SaveChanges();
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
