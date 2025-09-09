using TinyFeetBackend.DTOs.Paymnt;

namespace TinyFeetBackend.Services.Paymnt
{
    public interface IPaymentService
    {
        Task<PaymentResponseDto?> GetPaymentByOrderIdAsync(int orderId);
        Task<PaymentResponseDto> MakePaymentAsync(PaymentCreateDto dto);
    }
}
