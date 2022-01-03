using Sales.Core;
using Sales.Core.Abstractions;
using Sales.Entities.Abstractions;
using Sales.Entities.Factory;
using System.ServiceProcess;

namespace Sales.ServiceClient
{
    public partial class Service1 : ServiceBase
    {
        private readonly FolderWatcher _watcher;

        public Service1()
        {
            InitializeComponent();
            IEFContextFactory contextFactory = new EFContextFactory();
            IFileParse fileParse = new FileParse();
            IProcessManager manager = new ProcessManager(fileParse, contextFactory);

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
