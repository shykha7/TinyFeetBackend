namespace TinyFeetBackend.Entities
{
    public class Payment
    {
        public int Id { get; set; }

        // Link to Order
        public int OrderId { get; set; }
        public Order Order { get; set; }

        // Payment details
        public string PaymentMethod { get; set; } = "CreditCard"; // UPI, Card, COD, PayPal
        public decimal Amount { get; set; }
        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

        // Status
        public string Status { get; set; } = "Pending"; // Pending, Completed, Failed, Refunded
        public string TransactionId { get; set; } = Guid.NewGuid().ToString(); // Unique
    }
}
