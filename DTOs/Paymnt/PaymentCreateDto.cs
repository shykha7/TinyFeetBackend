namespace TinyFeetBackend.DTOs.Paymnt
{
    public class PaymentCreateDto
    {
        public int OrderId { get; set; }   // Linked order
        public string PaymentMethod { get; set; } = "Card"; // e.g., Card, UPI, COD
        public string CardNumber { get; set; } = string.Empty; // If card payment (last 4 digits)
        public string ExpiryDate { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public decimal Amount { get; set; } // Added amount field
        public string TransactionId { get; set; } = string.Empty; // Added transaction ID
    }
}
