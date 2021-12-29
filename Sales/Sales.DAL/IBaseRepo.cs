using System;
using System.Threading.Tasks;

namespace Sales.DAL
{
    public interface IBaseRepo<T> where T : class
    {
        Task Insert(T item);

        Task Remove(T item);

        Task Update(T item);

        Task<T> GetT(Func<T, bool> func);
    }
}
