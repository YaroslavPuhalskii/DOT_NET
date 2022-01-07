using Sales.Core;
using Sales.Core.Abstractions;
using Sales.Entities.Abstractions;
using Sales.Entities.Factory;
using System.ServiceProcess;

namespace Sales.ServiceClient
{
    public partial class Sales : ServiceBase
    {
        private readonly FolderWatcher _watcher;

        public Sales()
        {
            InitializeComponent();
            IEFContextFactory contextFactory = new EFContextFactory();
            IFileParser fileParser = new FileParser();
            IProcessManager manager = new ProcessManager(fileParser, contextFactory);

            _watcher = new FolderWatcher(manager);
        }

        protected override void OnStart(string[] args)
        {
            _watcher.Start();
        }

        protected override void OnStop()
        {
            if (_watcher != null)
            {
                _watcher.Stop();
            }
        }
    }
}
