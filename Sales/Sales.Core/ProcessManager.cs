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

        public ProcessManager(IFileParser fileParser, IEFContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _fileParser = fileParser;
        }

        public Task<bool> Run(string path)
        {
            if (string.IsNullOrEmpty(path))
            {                
                throw new ArgumentException(nameof(path));
            }

            return Task.Run(() =>
            {
                IFileReader fileReader = new FileReader(_fileParser);

                var data = fileReader.Read(path);

                _dataService = new DataService(_contextFactory);

                return _dataService.Save(data.Item1, data.Item2);              
            });
        }
    }
}
