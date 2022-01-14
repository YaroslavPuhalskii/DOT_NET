using Sales.Entities.Models;
using System.Data.Entity;

namespace Sales.Entities
{
    public class EFContext : DbContext
    {
        public EFContext()
            : base("name=EFContext")
        {
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Manager> Managers { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<FileData> FileDatas { get; set; }
    }
}