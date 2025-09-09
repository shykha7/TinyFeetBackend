using Microsoft.AspNetCore.Mvc;
using TinyFeetBackend.DTOs.Paymnt;
using TinyFeetBackend.Services.Paymnt;

namespace TinyFeetBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet("order/{orderId}")]
        public async Task<ActionResult<PaymentResponseDto>> GetPaymentByOrderId(int orderId)
        {
            var payment = await _paymentService.GetPaymentByOrderIdAsync(orderId);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult<PaymentResponseDto>> ProcessPayment(PaymentCreateDto dto)
        {
            try
            {
                var payment = await _paymentService.MakePaymentAsync(dto);
                return CreatedAtAction(nameof(GetPaymentByOrderId), new { orderId = payment.OrderId }, payment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}