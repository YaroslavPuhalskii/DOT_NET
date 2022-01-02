using Sales.Core;
using Sales.Core.Abstractions;
using Sales.Entities.Abstractions;
using Sales.Entities.Factory;
using System;

namespace Sales.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IEFContextFactory contextFactory = new EFContextFactory();
            IFileParse fileParse = new FileParse();
            IProcessManager manager = new ProcessManager(fileParse, contextFactory);

            var watcher = new FolderWatcher(manager);
            watcher.Start();

            Console.ReadLine();
        }
    }
}
