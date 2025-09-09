namespace TinyFeetBackend.DTOs.Paymnt
{
    public class PaymentResponseDto
    {
        public int PaymentId { get; set; }
        public int OrderId { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Status { get; set; } = "Pending"; // e.g., Pending, Success, Failed
        public string TransactionId { get; set; } = string.Empty; // from payment gateway
        public DateTime PaidAt { get; set; }
    }
}
    