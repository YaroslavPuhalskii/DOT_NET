using Sales.Entities;
using System;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Sales.DAL.Repositories
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected readonly DbContext _context = new EFContext();

        public async Task Insert(T item)
        {
            try
            {
                _context.Set<T>().Add(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{ex.Message}");
            }
        }

        public async Task Update(T item)
        {
            try
            {
                _context.Set<T>().Attach(item);
                _context.Entry(item).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{ex.Message}");
            }
        }

        public async Task Remove(T item)
        {
            try
            {
                _context.Set<T>().Remove(item);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"{ex.Message}");
            }

        }

        public async Task<T> GetT(Func<T, bool> func)
        {
            try
            {
                return await _context.Set<T>().FindAsync(func);
            }
            catch (Exception ex)
            {
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
