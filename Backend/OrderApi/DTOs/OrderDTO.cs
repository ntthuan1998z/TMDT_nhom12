using System;
using System.Collections.Generic;
using OrderApi.Models;

namespace OrderApi.DTOs
{
    public class OrderDTO
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime OrderDate { get; set; }
        public string Address { get; set; }
        public decimal Total { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<OrderItemDTO> Items { get; set; } = new List<OrderItemDTO>();
    }
}