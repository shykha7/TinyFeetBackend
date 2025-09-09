using System.ComponentModel.DataAnnotations;

namespace TinyFeetBackend.DTOs.Cart
{
    public class ChangeCartItemQuantityDto
    {

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Quantity must be between 0 and 100")]
        public int Quantity { get; set; }
    }
}
