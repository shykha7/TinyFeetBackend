using TinyFeetBackend.Entities;

namespace TinyFeetBackend.Repositories.Interface
{
    public interface IPaymentRepository
    {
        Task<Payment?> GetByOrderIdAsync(int orderId);
        Task<Payment> AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);
    }
}
