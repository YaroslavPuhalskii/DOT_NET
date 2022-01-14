using NLog;
using Sales.Core.Abstractions;
using Sales.Entities.Abstractions;
using System;
using System.Threading.Tasks;

namespace Sales.Core
{
    public class ProcessManager : IProcessManager
    {
        private readonly IEFContextFactory _contextFactory;

        private readonly IFileParser _fileParser;

        private IDataService _dataService;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public ProcessManager(IFileParser fileParser, IEFContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _fileParser = fileParser;
        }

        public Task Run(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                logger.Error($"{nameof(path)} can't be nu;; or empty!");
                throw new ArgumentException(nameof(path));
            }

            return Task.Run(() =>
            {
                IFileReader fileReader = new FileReader(_fileParser);

                var data = fileReader.Read(path);

                _dataService = new DataService(_contextFactory);

                _dataService.Save(data.Item1, data.Item2);
            });
        }
    }
}
