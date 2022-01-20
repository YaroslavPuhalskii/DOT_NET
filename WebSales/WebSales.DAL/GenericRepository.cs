using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebSales.DAL.Abstractions;

namespace WebSales.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext _context;

        private readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Delete(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{nameof(obj)} is null!");
            }

            if (_context.Entry(obj).State == EntityState.Detached)
            {
                _dbSet.Attach(obj);
            }

            _dbSet.Remove(obj);
        }

        public void Delete(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException($"{nameof(id)} is null!");
            }

            T entity = _dbSet.Find(id);

            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T GetById(object id)
        {
            if (id == null)
            {
                throw new ArgumentNullException($"{nameof(id)} is null!");
            }

            return _dbSet.Find(id);
        }

        public void Insert(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{nameof(obj)} is null!");
            }

            _dbSet.Add(obj);
        }

        public void Update(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException($"{nameof(obj)} is null!");
            }

            _dbSet.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
        }
    }
}
