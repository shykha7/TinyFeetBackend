using System.ComponentModel.DataAnnotations;

namespace TinyFeetBackend.DTOs.Products
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int Stock { get; set; }

        public string? ImageUrl { get; set; }

        public string? CategoryName { get; set; }
        public int? CategoryId { get; set; }
    }
}
