using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderApi.Models;

namespace OrderApi.Data
{
    public class SeedData
    {
        public static async Task Initialize(OrderContext context)
        {
            await context.Database.EnsureCreatedAsync();

            if (!context.Orders.Any())
            {
                await context.AddAsync(new Order
                {
                    CustomerId = "1",
                    CustomerName = "Alice",
                    OrderDate = DateTime.Now,
                    Status = OrderStatus.Preparing,
                    Address = "test",
                    Total = 1000,
                    Items = new List<OrderItem> {
                        new OrderItem {
                            ProductId = 1,
                            Units = 2,
                            UnitPrice = 1000,
                        }
                    }
                });

                await context.SaveChangesAsync();
            }
        }
    }
}