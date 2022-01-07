using System;
using System.Linq.Expressions;

namespace Sales.DAL
{
    public interface IBaseRepo<T> where T : class
    {
        void Insert(T item);

        void Remove(T item);

        void Remove(int item);

        void Update(T item);

        T Get(Expression<Func<T, bool>> func);
    }
}
