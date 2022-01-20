using System;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Models;

namespace WebSales.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _context = new EFContext();

        private bool disposedValue;

        private readonly IGenericRepository<Client> _clientRepo;

        private readonly IGenericRepository<Manager> _managerRepo;

        private readonly IGenericRepository<Product> _productRepo;

        private readonly IGenericRepository<Sale> _saleRepo;

        public IGenericRepository<Client> ClientRepo => _clientRepo ?? new GenericRepository<Client>(_context);

        public IGenericRepository<Manager> ManagerRepo => _managerRepo ?? new GenericRepository<Manager>(_context);

        public IGenericRepository<Product> ProductRepo => _productRepo ?? new GenericRepository<Product>(_context);

        public IGenericRepository<Sale> SaleRepo => _saleRepo ?? new GenericRepository<Sale>(_context);

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
