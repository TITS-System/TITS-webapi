using System;
using Microsoft.EntityFrameworkCore;
using Models.Db;
using Models.Db.Account;
using Models.Db.Sessions;

namespace Infrastructure
{
    public class TitsDbContext : DbContext
    {
        public TitsDbContext()
        {
        }

        public TitsDbContext(DbContextOptions<TitsDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            var connectionString = Environment.GetEnvironmentVariable("CONN_STR");

            if (connectionString == null) throw new ArgumentNullException("env:CONN_STR NOT PASSED");

            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<WorkerAccountToRole>().HasKey(atr => new {atr.WorkerRoleId, atr.WorkerAccountId});
            // modelBuilder.Entity<LatLng>().HasOne(atr => atr.Order).WithOne(o => o.DestinationLatLng).IsRequired(false).HasForeignKey<LatLng>(latlng => latlng.OrderId);
            // modelBuilder.Entity<LatLng>().HasOne(atr => atr.Restaurant).WithOne(r => r.LocationLatLng).IsRequired(false).HasForeignKey<LatLng>(latlng => latlng.RestaurantId);
            // modelBuilder.Entity<LatLng>().HasOne(atr => atr.Delivery).WithMany(d => d.LatLngs).IsRequired(false).HasForeignKey(latlng => latlng.DeliveryId);
        }

        public DbSet<WorkerAccount> WorkerAccounts { get; set; }
        public DbSet<TokenSession> TokenSessions { get; set; }
        public DbSet<WorkerAccountToRole> WorkerAccountToRoles { get; set; }
        public DbSet<WorkerRole> WorkerRoles { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<LatLng> LatLngs { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
    }
}