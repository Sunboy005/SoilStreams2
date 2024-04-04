using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Models.Dto;

namespace SOILSTREAMAPI.Data
{
    public class SoilStreamsDbContext : IdentityDbContext<User>
    {
        public SoilStreamsDbContext(DbContextOptions<SoilStreamsDbContext> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure the relationship without cascade delete
            modelBuilder.Entity<SingleOrderDto>()
                .HasOne(o => o.StoreProduct)
                .WithMany()
                .HasForeignKey(o => o.StoreProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
    
}
