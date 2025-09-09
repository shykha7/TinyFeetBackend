using Microsoft.AspNetCore.Mvc;
using TinyFeetBackend.DTOs.Orders;
using TinyFeetBackend.Services.Ord;

namespace TinyFeetBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponseDto>> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<OrderResponseDto>>> GetByUserId(int userId)
        {
            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<ActionResult<OrderResponseDto>> PlaceOrder(OrderCreateDto dto)
        {
            var order = await _orderService.PlaceOrderAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = order.OrderId }, order);
        }

       

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.DeleteOrderAsync(id);
            return NoContent();
        }
    }
}