using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderApi.Infrastructure;
using OrderApi.Models;

namespace OrderApi.Data.Repositories
{
    public class OrderRepository : EFRepository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Order>> GetByCustomerAsync(string customerId)
        {
            return await Context.Orders
                                .Where(o => o.CustomerId == customerId)
                                .ToListAsync();
        }

        private OrderContext Context => Database as OrderContext;
    }
}