using NLog;
using Sales.Core.Abstractions;
using Sales.DAL;
using Sales.DAL.Repositories;
using Sales.Entities.Abstractions;
using Sales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Sales.Core
{
    public class DataService : IDataService
    {
        private readonly IEFContextFactory _contextFactory;

        private IFileDataRepo _fileDataRepo;

        private IManagerRepo _managerRepo;

        private IClientRepo _clientRepo;

        private IProductRepo _productRepo;

        private ISaleRepo _saleRepo;

        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static readonly object locker = new object();

        public DataService(IEFContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void Save(FileData fileData, IEnumerable<FormatLine> formatLines)
        {
            using (var context = _contextFactory.GetContext())
            {
                Init(context);
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        SaveData(fileData, formatLines);

                        transaction.Commit();
                        logger.Info($"Commit : {fileData.Manager.Name}_{fileData.DateCreate.ToShortDateString()}.csv");
                    }
                    catch (Exception ex)
                    {
                        logger.Error($"Rollback! Data can't be added! {ex.Message}");

                        transaction.Rollback();
                    }
                }
            }
        }

        private void Init(DbContext context)
        {
            _fileDataRepo = new FileDataRepo(context);

            _managerRepo = new ManagerRepo(context);

            _clientRepo = new ClientRepo(context);

            _productRepo = new ProductRepo(context);

            _saleRepo = new SaleRepo(context);
        }

        private void SaveData(FileData fileData, IEnumerable<FormatLine> formatLines)
        {
            lock (locker)
            {
                var manager = _managerRepo.Get(x => x.Name == fileData.Manager.Name);

                if (manager != null)
                {
                    fileData.Manager = manager;
                }

                _fileDataRepo.Insert(fileData);

                foreach (var line in formatLines)
                {
                    var client = _clientRepo.Get(x => x.Name.Equals(line.Client.Name));
                    var product = _productRepo.Get(x => x.Name.Equals(line.Product.Name));

                    if (client != null)
                    {
                        line.Client = client;
                    }

                    if (product != null)
                    {
                        line.Product = product;
                    }

                    _saleRepo.Insert(new Sale()
                    {
                        DateTime = line.DateTime,
                        Client = line.Client,
                        Product = line.Product,
                        FileData = fileData,
                        Sum = line.Sum
                    });
                }
            }
        }
    }
}
