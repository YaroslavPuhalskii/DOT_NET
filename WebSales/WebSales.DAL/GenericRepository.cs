using NLog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using WebSales.DAL.Abstractions;

namespace WebSales.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;

        private readonly DbSet<T> _dbSet;

        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        public DbSet<T> GetDbSet => _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Delete(T obj)
        {
            if (obj == null)
            {
                _logger.Error($"Failed to delete : {nameof(obj)} is null!");
                throw new ArgumentNullException($"{nameof(obj)} is null!");
            }

            if (_context.Entry(obj).State == EntityState.Detached)
            {
                _dbSet.Attach(obj);
            }

            _dbSet.Remove(obj);
        }

        public async Task Delete(object id)
        {
            if (id == null)
            {
                _logger.Error($"Failed to delete : {nameof(id)} is null!");
                throw new ArgumentNullException($"{nameof(id)} is null!");
            }

            T entity = await _dbSet.FindAsync(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(object id)
        {
            if (id == null)
            {
                _logger.Error($"Failed to get object by id : {nameof(id)} is null!");
                throw new ArgumentNullException($"{nameof(id)} is null!");
            }

            return await _dbSet.FindAsync(id);
        }

        public void Insert(T obj)
        {
            if (obj == null)
            {
                _logger.Error($"Failed to insert : {nameof(obj)} is null!");
                throw new ArgumentNullException($"{nameof(obj)} is null!");
            }

            _dbSet.Add(obj);
        }

        public void Update(T obj)
        {
            if (obj == null)
            {
                _logger.Error($"Failed to update : {nameof(obj)} is null!");
                throw new ArgumentNullException($"{nameof(obj)} is null!");
            }

            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
