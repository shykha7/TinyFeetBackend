namespace TinyFeetBackend.DTOs.Cart
{
    public class CartResponseDto
    {

        public int Id { get; set; }
        public int UserId { get; set; }
        public List<CartItemResponseDto> Items { get; set; } = new List<CartItemResponseDto>();
    }
}
