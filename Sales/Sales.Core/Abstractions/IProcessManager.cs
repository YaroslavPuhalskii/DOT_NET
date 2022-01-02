using System.Threading.Tasks;

namespace Sales.Core.Abstractions
{
    public interface IProcessManager
    {
        Task<bool> Run(string path);
    }
}
