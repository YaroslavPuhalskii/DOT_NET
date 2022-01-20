using System.Data.Entity;
using WebSales.DAL.Models;

namespace WebSales.DAL
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
    }
}