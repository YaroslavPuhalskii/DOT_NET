using Sales.Core.Abstractions;
using System;
using System.Configuration;
using System.IO;

namespace Sales.Core
{
    public class FolderWatcher : IDisposable
    {
        private readonly string startFolder = ConfigurationManager.AppSettings["forManager"];

        private readonly string processingFolder = ConfigurationManager.AppSettings["processing"];

        private readonly string processedFolder = ConfigurationManager.AppSettings["processed"];

        private FileSystemWatcher _watcher;

        private bool disposedValue;

        private readonly IProcessManager _processManager;

        public FolderWatcher(IProcessManager processManager)
        {
            _processManager = processManager;
        }

        public void Start()
        {
            Init();
        }

        public void Stop()
        {
            Dispose();
        }

        private void Init()
        {
            _watcher = new FileSystemWatcher(startFolder)
            {
                NotifyFilter = NotifyFilters.Attributes
                                | NotifyFilters.DirectoryName
                                | NotifyFilters.FileName
            };


            _watcher.Created += OnCreated;

            _watcher.Filter = "*.csv";
            _watcher.EnableRaisingEvents = true;

        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            string path = string.Concat(processingFolder, e.Name);

            File.Move(e.FullPath, path);

            _processManager.Run(path).ContinueWith(x =>
            {
                if (x.Result)
                {
                    File.Move(path, string.Concat(processedFolder, e.Name));
                }
            });
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _watcher.Created -= OnCreated;
                    _watcher.EnableRaisingEvents = false;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
