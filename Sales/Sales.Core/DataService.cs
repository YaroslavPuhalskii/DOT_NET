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

        private IFileDataRepo fileDataRepo;

        private IManagerRepo managerRepo;

        private IClientRepo clientRepo;

        private IProductRepo productRepo;

        private ISaleRepo saleRepo;

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
                        logger.Info("Rollback!");
                        logger.Error($"{ex.Message}");

                        transaction.Rollback();
                    }
                }
            }
        }

        private void Init(DbContext context)
        {
            fileDataRepo = new FileDataRepo(context);

            managerRepo = new ManagerRepo(context);

            clientRepo = new ClientRepo(context);

            productRepo = new ProductRepo(context);

            saleRepo = new SaleRepo(context);
        }

        private void SaveData(FileData fileData, IEnumerable<FormatLine> formatLines)
        {
            lock (locker)
            {
                var manager = managerRepo.Get(x => x.Name == fileData.Manager.Name);

                if (manager != null)
                {
                    fileData.Manager = manager;
                }

                fileDataRepo.Insert(fileData);

                foreach (var line in formatLines)
                {
                    var client = clientRepo.Get(x => x.Name.Equals(line.Client.Name));
                    var product = productRepo.Get(x => x.Name.Equals(line.Product.Name));

                    if (client != null)
                    {
                        line.Client = client;
                    }

                    if (product != null)
                    {
                        line.Product = product;
                    }

                    saleRepo.Insert(new Sale()
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
