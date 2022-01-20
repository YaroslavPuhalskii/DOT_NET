using System.Collections.Generic;

namespace WebSales.DAL.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(object id);

        void Insert(T obj);

        void Delete(object id);

        void Delete(T obj);

        void Update(T obj);
    }
}
