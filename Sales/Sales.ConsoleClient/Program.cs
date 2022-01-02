using Sales.Core;
using Sales.DAL;
using Sales.DAL.Repositories;
using Sales.Entities;
using Sales.Entities.Abstractions;
using Sales.Entities.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (var context = new EFContext())
            //{
            //    var sale = new SaleRepo(context);

            //    sale.Remove(sale.GetT(x => x.Id == 3));
            //}


            //IEFContextFactory _context = new EFContextFactory();
            //using (var context = _context.GetContext())
            //{
            //    IManagerRepo manager1 = new ManagerRepo(context);

            //    manager1.Remove(manager1.GetT(x => x.Id == 1));
            //}

            //    IEFContextFactory context = new EFContextFactory();

            //IManagerRepo manager1 = new ManagerRepo(context);
            //manager1.Remove(1);
            //manager1.Remove(2);
            //manager1.Remove(3);
            //manager1.Remove(4);
            ////manager1.Remove(5);

            ProcessManager manager = new ProcessManager(new FileParse(), new FileLoader(_context.GetContext()));

            var watcher = new FolderWatcher(manager);
            watcher.Run();

            Console.ReadLine();
        }
    }
}
