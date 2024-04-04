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
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<StoreProduct> StoreProducts { get; set; }
        public DbSet<Store> Stores { get; set; }

        

    }
    
}
