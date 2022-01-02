using Sales.Core.Abstractions;
using Sales.Entities.Abstractions;
using Sales.Entities.Factory;
using System;
using System.Threading.Tasks;

namespace Sales.Core
{
    public class ProcessManager : IProcessManager
    {
        private readonly IEFContextFactory _contextFactory;

        private readonly IFileParse _fileParse;

        private IFileLoader _fileLoader;

        public ProcessManager(IFileParse fileParse, IEFContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
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
                using (var context = _contextFactory.GetContext())
                {
                    using (var transaction = context.Database.BeginTransaction())
                    {
                        try
                        {
                            _fileLoader = _fileLoader ?? new FileLoader(context);

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
