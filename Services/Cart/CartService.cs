using TinyFeetBackend.DTOs.Cart;
using TinyFeetBackend.Entities.Cart;
using TinyFeetBackend.Repositories.Interface;
using TinyFeetBackend.Services.Interfaces;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;

    public CartService(ICartRepository cartRepository)
    {
        _cartRepository = cartRepository;
    }

    // Add to cart
    public async Task<Cart> AddToCart(int userId, AddToCartDto dto)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId) ?? new Cart { UserId = userId };

        var item = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
        if (item != null) item.Quantity += dto.Quantity;
        else cart.Items.Add(new CartItem { ProductId = dto.ProductId, Quantity = dto.Quantity });

        if (cart.Id == 0) await _cartRepository.AddCartAsync(cart);
        await _cartRepository.SaveChangesAsync();
        return cart;
    }

    // Decrease quantity
    public async Task<Cart> DecreaseQuantity(int userId, ChangeCartItemQuantityDto dto)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) return null;

        var item = cart.Items.FirstOrDefault(i => i.ProductId == dto.ProductId);
        if (item == null) return cart;

        item.Quantity -= dto.Quantity;
        if (item.Quantity <= 0) cart.Items.Remove(item);

        await _cartRepository.SaveChangesAsync();
        return cart;
    }

    // Remove item
    public async Task<Cart> RemoveItem(int userId, int productId)
    {
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);
        if (cart == null) return null;

        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null) cart.Items.Remove(item);

        await _cartRepository.SaveChangesAsync();
        return cart;
    }

    // Clear cart
    public async Task<Cart> ClearCart(int userId)
    {
        
        var cart = await _cartRepository.GetCartByUserIdAsync(userId);

        if (cart == null) return null;

      
        cart.Items.Clear();

       
        await _cartRepository.SaveChangesAsync();

       
        cart = await _cartRepository.GetCartByUserIdAsync(userId);

        return cart;
    }
  
    public async Task<Cart> GetCart(int userId) =>
        await _cartRepository.GetCartByUserIdAsync(userId);
}
