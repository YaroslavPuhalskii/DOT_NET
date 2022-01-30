using System;
using System.Threading.Tasks;
using WebSales.DAL.Abstractions;
using WebSales.DAL.Repositories;

namespace WebSales.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EFContext _context = new EFContext();

        private bool disposedValue;

        private IClientRepo _clientRepo;

        private IManagerRepo _managerRepo;

        private IProductRepo _productRepo;

        private ISaleRepo _saleRepo;

        public IClientRepo GetClientRepo => _clientRepo = _clientRepo ?? new ClientRepo(_context);

        public IManagerRepo GetManagerRepo => _managerRepo = _managerRepo ?? new ManagerRepo(_context);

        public IProductRepo GetProductRepo => _productRepo = _productRepo ?? new ProductRepo(_context);

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
