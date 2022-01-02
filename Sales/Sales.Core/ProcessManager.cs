using Sales.Entities.Abstractions;
using Sales.Entities.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sales.Core
{
    public class ProcessManager
    {
        private readonly IEFContextFactory _context;

        private readonly FileParse _fileParse;

        private readonly FileLoader _fileLoader;

        public ProcessManager(FileParse fileParse, FileLoader loader)
        {
            _context = new EFContextFactory();
            _fileLoader = loader;
            _fileParse = fileParse;
        }

        public Task<bool> Run(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException(nameof(path));
            }

            return Task.Run(() =>
            {
                using (var context = _context.GetContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            var data = _fileParse.Parse(path);

                            _fileLoader.Add(data.Item1, data.Item2);

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }

                    return true;
            });
        }
    }
}
