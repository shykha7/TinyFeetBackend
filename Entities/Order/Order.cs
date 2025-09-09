

namespace TinyFeetBackend.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public User User { get; set; }

        // Who placed the order
        public int UserId { get; set; }

        // Order details
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public decimal TotalAmount { get; set; }  // final calculated amount
  

        // Navigation property
        public List<OrderItem> Items { get; set; } = new();
        public Payment Payment { get; set; }  // one-to-one with Payment
    }
}
