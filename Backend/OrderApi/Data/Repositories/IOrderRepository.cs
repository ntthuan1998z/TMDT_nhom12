using System.Collections.Generic;
using System.Threading.Tasks;
using OrderApi.Infrastructure;
using OrderApi.Models;

namespace OrderApi.Data.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetByCustomerAsync(string customerId);
    }
}