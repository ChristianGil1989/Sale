using Microsoft.EntityFrameworkCore;
using Sales.API.Models;
using System.Diagnostics.Metrics;

namespace Sales.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {   
        }
        public DbSet <Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
        }
    }
}
