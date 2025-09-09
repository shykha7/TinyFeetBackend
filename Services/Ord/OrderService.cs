using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TinyFeetBackend.Data;
using TinyFeetBackend.DTOs.Orders;
using TinyFeetBackend.Entities;
using TinyFeetBackend.Repositories.Interface;
using TinyFeetBackend.Services.Ord;

namespace TinyFeetBackend.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public OrderService(IOrderRepository orderRepository, IMapper mapper, AppDbContext context)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<OrderResponseDto?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return _mapper.Map<OrderResponseDto>(order);
        }

        public async Task<IEnumerable<OrderResponseDto>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _orderRepository.GetAllByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
        }

        public async Task<OrderResponseDto> PlaceOrderAsync(OrderCreateDto dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                Items = new List<OrderItem>()
            };

            foreach (var itemDto in dto.Items)
            {
                var product = await _context.Products.FindAsync(itemDto.ProductId);

                // Add debug logging
                Console.WriteLine($"Product ID: {itemDto.ProductId}, Found: {product != null}");
                if (product != null)
                {
                    Console.WriteLine($"Product Name: {product.Name}, Price: {product.Price}, Image: {product.ImageUrl}");
                }
                else
                {
                    Console.WriteLine($"Product with ID {itemDto.ProductId} not found!");
                    continue;
                }

                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ImageUrl = product.ImageUrl,
                    Price = product.Price,
                    Quantity = itemDto.Quantity
                });
            }

            var createdOrder = await _orderRepository.AddAsync(order);
            return _mapper.Map<OrderResponseDto>(createdOrder);
        }




        public async Task DeleteOrderAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);
        }
    }
}