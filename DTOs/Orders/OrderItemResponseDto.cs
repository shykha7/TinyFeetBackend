﻿namespace TinyFeetBackend.DTOs.Orders
{
    public class OrderItemResponseDto
    {
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}