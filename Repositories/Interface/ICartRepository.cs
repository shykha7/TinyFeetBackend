﻿using TinyFeetBackend.Entities.Cart;

namespace TinyFeetBackend.Repositories.Interface
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync(int userId);
        Task AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task SaveChangesAsync();



    }
}
