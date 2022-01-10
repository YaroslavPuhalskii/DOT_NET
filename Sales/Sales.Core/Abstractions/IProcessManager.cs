using System.Threading.Tasks;

namespace Sales.Core.Abstractions
{
    public interface IProcessManager
    {
        Task Run(string path);
    }
}
