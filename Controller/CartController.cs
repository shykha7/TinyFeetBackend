using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TinyFeetBackend.DTOs.Cart;
using TinyFeetBackend.Services.Interfaces;

namespace TinyFeetBackend.Controllers
{
    [Route("api/Cart")]
    [ApiController]
    [Authorize(Roles = "User")] // Only users can access
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        // 🔹 Helper to get UserId from JWT
        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        }

        // 🔹 Token validation endpoint
        [HttpGet("validate")]
        public IActionResult ValidateToken()
        {
            var userId = GetUserId();
            return Ok(new { valid = true, userId = userId });
        }

        // 🔹 Get Cart
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();
            var cart = await _cartService.GetCart(userId);
            return Ok(cart);
        }

        // 🔹 Add to Cart
        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartDto dto)
        {
            var userId = GetUserId();
            var cart = await _cartService.AddToCart(userId, dto);
            return Ok(cart);
        }

        // 🔹 Decrease Quantity
        [HttpPut("decrease")]
        public async Task<IActionResult> DecreaseQuantity([FromBody] ChangeCartItemQuantityDto dto)
        {
            var userId = GetUserId();
            var cart = await _cartService.DecreaseQuantity(userId, dto);
            return Ok(cart);
        }

        // 🔹 Remove Item
        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveItem(int productId)
        {
            var userId = GetUserId();
            var cart = await _cartService.RemoveItem(userId, productId);
            return Ok(cart);
        }

        // 🔹 Clear Cart
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = GetUserId();
            var cart = await _cartService.ClearCart(userId);
            return Ok(cart);
        }
    }
}