using Microsoft.EntityFrameworkCore;
using TinyFeetBackend.Data;
using TinyFeetBackend.Entities.Cart;
using TinyFeetBackend.Repositories.Interface;

namespace TinyFeetBackend.Repositories.Implementations
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.User)
                .Include(c => c.Items)
                    .ThenInclude(i => i.Product) // Make sure this includes the product
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
        public async Task AddCartAsync(Cart cart)
        {
            await _context.Carts.AddAsync(cart);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            _context.Carts.Update(cart);
        }

      
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
