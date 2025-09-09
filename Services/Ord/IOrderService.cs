using TinyFeetBackend.DTOs.Orders;

namespace TinyFeetBackend.Services.Ord
{
    public interface IOrderService
    {
        Task<OrderResponseDto?> GetOrderByIdAsync(int id);
        Task<IEnumerable<OrderResponseDto>> GetOrdersByUserIdAsync(int userId);
        Task<OrderResponseDto> PlaceOrderAsync(OrderCreateDto dto);
        Task DeleteOrderAsync(int id);
    }
}
