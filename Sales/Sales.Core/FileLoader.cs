using Sales.Core.Abstractions;
using Sales.DAL;
using Sales.DAL.Repositories;
using Sales.Entities.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace Sales.Core
{
    public class FileLoader : IFileLoader
    {
        private readonly IFileDataRepo fileDataRepo;
        private readonly IManagerRepo managerRepo;
        private readonly IClientRepo clientRepo;
        private readonly IProductRepo productRepo;
        private readonly ISaleRepo saleRepo;

        public FileLoader(DbContext context)
        {
            fileDataRepo = new FileDataRepo(context);
            managerRepo = new ManagerRepo(context);
            clientRepo = new ClientRepo(context);
            productRepo = new ProductRepo(context);
            saleRepo = new SaleRepo(context);
        }

        public void Add(FileData fileData, IEnumerable<FormatLine> formatLines)
        {
            if (fileData == null)
            {
                throw new ArgumentNullException(nameof(fileData));
            }

            var manager = managerRepo.GetT(x => x.Name == fileData.Manager.Name);

            if (manager != null)
            {
                fileData.Manager = manager;
            }

            fileDataRepo.Insert(fileData);

            foreach (var line in formatLines)
            {
                var client = clientRepo.GetT(x => x.Name.Equals(line.Client.Name));
                var product = productRepo.GetT(x => x.Name.Equals(line.Product.Name));

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
