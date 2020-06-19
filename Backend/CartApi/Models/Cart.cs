using System.Collections.Generic;

namespace CartApi.Models
{
    public class Cart
    {
        public Cart() {}
        public Cart(string customerId) {
            Id = customerId;
        }

        public string Id { get; set; }

        public IEnumerable<CartItem> Items { get; set; } = new List<CartItem>();
    }
}