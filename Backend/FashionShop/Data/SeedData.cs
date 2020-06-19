using System.Linq;
using System.Threading.Tasks;
using FashionShop.models;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Data
{
    public class SeedData
    {
        public static async Task Initialize(EShopContext context) {
            //context.Database.Migrate();
            context.Database.EnsureCreated();

            if(!context.ProductType.Any()) {
                context.ProductType.AddRange(
                    new ProductType {
                        Id = 1,
                        Name = "Other"
                    },
                    new ProductType {
                        Id = 2,
                        Name = "Men"
                    }
                );
                await context.SaveChangesAsync();
            }

            if(!context.Products.Any()) {
                context.Products.AddRange(
                    new Product {
                        Name = "sp1",
                        Price = 5000,
                        TypeId = 1,
                    },
                    new Product {
                        Name = "sp2",
                        Price = 5000,
                        TypeId = 1,
                    }
                );
                await context.SaveChangesAsync();
            }
         }
    }
}