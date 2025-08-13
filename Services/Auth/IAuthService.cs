
using TinyFeetBackend.DTOs.Auth;

namespace TinyFeetBackend.Services.Auth
{
    public interface IAuthService
    {
        public Task<AuthResponseDto> LoginAsync(UserLoginDto loginDto);
        public Task<AuthResponseDto> RegisterAsync(UserRegistrationDto registerDto);
    }
}
