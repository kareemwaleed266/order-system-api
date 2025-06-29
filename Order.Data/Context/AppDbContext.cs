
using Microsoft.EntityFrameworkCore;
using Order.Data.Entities;
using Order.Data.Entities.CustomerEntities;
using Order.Data.Entities.OrderEntities;
using Order.Data.Entities.ProductEntities;
using System.Reflection;

namespace Order.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<OrderItems> OrderItems { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
