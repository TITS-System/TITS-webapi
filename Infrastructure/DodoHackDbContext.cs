using System;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure
{
    public class DodoHackDbContext : DbContext
    {
        public DodoHackDbContext()
        {
        }

        public DodoHackDbContext(DbContextOptions<DodoHackDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            
            var connectionString = Environment.GetEnvironmentVariable("CONN_STR");

            if (connectionString == null) throw new ArgumentNullException("env:CONN_STR NOT PASSED");

            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<LatLng> LatLngs { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }
        
        public DbSet<OrderProductTemplate> OrderProductsTemplates { get; set; }

        public DbSet<Courier> Couriers { get; set; }
    }
}