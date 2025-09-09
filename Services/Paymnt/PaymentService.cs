using AutoMapper;
using TinyFeetBackend.DTOs.Paymnt;
using TinyFeetBackend.Entities;
using TinyFeetBackend.Repositories.Interface;
using TinyFeetBackend.Services.Paymnt;

namespace TinyFeetBackend.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMapper _mapper;

        public PaymentService(IPaymentRepository paymentRepository, IMapper mapper)
        {
            _paymentRepository = paymentRepository;
            _mapper = mapper;
        }

        public async Task<PaymentResponseDto?> GetPaymentByOrderIdAsync(int orderId)
        {
            var payment = await _paymentRepository.GetByOrderIdAsync(orderId);
            return _mapper.Map<PaymentResponseDto>(payment);
        }

        public async Task<PaymentResponseDto> MakePaymentAsync(PaymentCreateDto dto)
        {
            // Validate required fields
            if (string.IsNullOrEmpty(dto.CardNumber) || dto.CardNumber.Length < 4)
            {
                throw new Exception("Invalid card number");
            }

            if (dto.Amount <= 0)
            {
                throw new Exception("Amount must be greater than 0");
            }

            // Process payment logic here
            var payment = new Payment
            {
                OrderId = dto.OrderId,
                PaymentMethod = dto.PaymentMethod,
                Amount = dto.Amount,
                Status = "Completed",
                TransactionId = dto.TransactionId,
                PaidAt = DateTime.UtcNow
            };

            // Save to database and return response
            return _mapper.Map<PaymentResponseDto>(payment);
        }
    }
}
