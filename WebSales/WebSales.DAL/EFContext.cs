using System.ComponentModel.DataAnnotations.Schema;
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Client>()
                .Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Client>()
                .HasMany(x => x.Sales).WithRequired(x => x.Client).WillCascadeOnDelete(false);

            modelBuilder.Entity<Manager>()
                .HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Manager>()
                .Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Manager>()
                .HasMany(x => x.Sales).WithRequired(x => x.Manager).WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<Product>()
                .Property(x => x.Name).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>()
                .Property(x => x.Category).IsRequired().HasMaxLength(50);
            modelBuilder.Entity<Product>()
                .HasMany(x => x.Sales).WithRequired(x => x.Product).WillCascadeOnDelete(false);

            modelBuilder.Entity<Sale>()
                .HasKey(x => x.Id).Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}