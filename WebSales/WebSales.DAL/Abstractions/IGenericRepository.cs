using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebSales.DAL.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(object id);

        void Insert(T obj);

        Task Delete(object id);

        void Delete(T obj);

        void Update(T obj);
    }
}
