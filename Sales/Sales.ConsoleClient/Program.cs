using Sales.Core;
using Sales.Entities.Abstractions;
using Sales.Entities.Factory;
using System;

namespace Sales.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IEFContextFactory _context = new EFContextFactory();

            ProcessManager manager = new ProcessManager(new FileParse(), new FileLoader(_context.GetContext()));

            var watcher = new FolderWatcher(manager);
            watcher.Run();

            Console.ReadLine();
        }
    }
}
