namespace OrderApi.DTOs
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Units { get; set; }
    }
}