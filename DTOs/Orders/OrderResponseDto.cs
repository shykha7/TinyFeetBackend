namespace TinyFeetBackend.DTOs.Orders
{
    public class OrderResponseDto
    {
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItemResponseDto> Items { get; set; } = new();
        public DateTime CreatedAt { get; set; }
    }
}