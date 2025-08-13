namespace TinyFeetBackend.DTOs.Auth
{
    public class AuthResponseDto
    {
        public UserResponseDto User { get; set; } = new UserResponseDto();
        public string Token { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
