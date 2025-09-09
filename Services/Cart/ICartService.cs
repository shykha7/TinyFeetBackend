using TinyFeetBackend.Entities.Cart;
using TinyFeetBackend.DTOs.Cart;


namespace TinyFeetBackend.Services.Interfaces
{
    public interface ICartService
    {
        Task<Cart> AddToCart(int userId, AddToCartDto dto);
        Task<Cart> DecreaseQuantity(int userId, ChangeCartItemQuantityDto dto);
        Task<Cart> RemoveItem(int userId, int productId);
        Task<Cart> ClearCart(int userId);
        Task<Cart> GetCart(int userId);
    }
}
