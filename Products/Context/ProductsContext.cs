using Microsoft.EntityFrameworkCore;
using Products.Models;

namespace Products.Context
{
    public class ProductsContext : DbContext
    {
        public ProductsContext(DbContextOptions<ProductsContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product(Guid.NewGuid(), "admin", "admin", 123, "BR")
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("InitialConnection"));
        }

        public DbSet<Product> Products { get; set; }
    }
}
