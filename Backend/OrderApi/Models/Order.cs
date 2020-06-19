using System;
using System.Collections.Generic;

namespace OrderApi.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate  { get; set; }
        public OrderStatus Status { get; set; }
        public string Address { get; set; }
        public decimal Total { get; set; }
        public string PhoneNumber { get; set; }
        public virtual IEnumerable<OrderItem> Items { get; set; } = new List<OrderItem>();
    }

    public enum OrderStatus {
        Preparing = 0,
        Shipped = 1,
        Delivered = 2
    }
}