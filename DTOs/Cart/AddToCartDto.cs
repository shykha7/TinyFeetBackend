﻿using System.ComponentModel.DataAnnotations;

namespace TinyFeetBackend.DTOs.Cart
{
    public class AddToCartDto
    {

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }
    }
}
