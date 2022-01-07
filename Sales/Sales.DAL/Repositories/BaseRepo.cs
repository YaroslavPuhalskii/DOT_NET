using NLog;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Sales.DAL.Repositories
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        protected readonly DbContext _context;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public BaseRepo(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void Insert(T item)
        {
            try
            {
                _context.Set<T>().Add(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ArgumentException(ex.Message);
            }
        }

        public void Update(T item)
        {
            try
            {
                _context.Set<T>().Attach(item);
                _context.Entry(item).State = EntityState.Modified;
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ArgumentException(ex.Message);
            }
        }

        public void Remove(T item)
        {
            try
            {
                _context.Set<T>().Remove(item);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void Remove(int item)
        {
            try
            {
                T t = _context.Set<T>().Find(item);
                _context.Set<T>().Remove(t);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ArgumentException(ex.Message);
            }
        }

        public T Get(Expression<Func<T, bool>> func)
        {
            try
            {
                return _context.Set<T>().FirstOrDefault(func);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
                throw new ArgumentException(ex.Message);
            }
        }
    }
}
