using System;
using Microsoft.EntityFrameworkCore;
using Models.Db;

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
            modelBuilder.Entity<AccountToRole>().HasKey(atr => new {atr.RoleId, atr.AccountId});
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<AccountToRole> AccountToRoles { get; set; }
        public DbSet<AccountSession> AccountSessions { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<IngredientTemplate> IngredientTemplates { get; set; }
        public DbSet<ProductTemplate> ProductTemplates { get; set; }
        public DbSet<ProductPackTemplate> ProductPackTemplates { get; set; }

        public DbSet<OrderIngredient> OrderIngredients { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<OrderProductPack> OrderProductPacks { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<WorkPoint> WorkPoints { get; set; }
        public DbSet<WorkSession> WorkSessions { get; set; }
        public DbSet<WorkSessionPause> WorkSessionPauses { get; set; }
        public DbSet<ScheduledWorkSession> ScheduledWorkSessions { get; set; }
        
        public DbSet<LatLng> LatLngs { get; set; }
    }
}