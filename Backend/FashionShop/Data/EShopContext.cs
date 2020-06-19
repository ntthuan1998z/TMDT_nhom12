using FashionShop.models;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Data
{
    public class EShopContext : DbContext
    {
        public EShopContext(DbContextOptions<EShopContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }

        public DbSet<ProductType> ProductType { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                    .HasOne<ProductType>()
                    .WithMany()
                    .HasForeignKey(p => p.TypeId);
        }
    }
}