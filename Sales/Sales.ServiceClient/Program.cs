using System.ServiceProcess;

namespace Sales.ServiceClient
{
    static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Sales()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
