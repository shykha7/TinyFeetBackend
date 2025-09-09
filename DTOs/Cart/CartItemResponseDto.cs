﻿namespace TinyFeetBackend.DTOs.Cart
{
    public class CartItemResponseDto
    {

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
    }
}
