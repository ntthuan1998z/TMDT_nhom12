using CartApi.Models;
using StackExchange.Redis;

namespace CartApi.Infrastructure
{
    public class CartRepository : RedisRepository<Cart>, ICartRepository
    {
        public CartRepository(ConnectionMultiplexer redis) : base(redis.GetDatabase()) {
            
        }
    }
}