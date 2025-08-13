
using TinyFeetBackend.DTOs;
using TinyFeetBackend.DTOs.Auth;
using TinyFeetBackend.Entities;
using TinyFeetBackend.Helpers.Interfaces;
using TinyFeetBackend.Repositories.Interface;

namespace TinyFeetBackend.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtHelper _jwtHelper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUserRepository userRepository, IJwtHelper jwtHelper, ILogger<AuthService> logger)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
            _logger = logger;
        }

        public async Task<AuthResponseDto> LoginAsync(UserLoginDto loginDto)
        {
            var user = await _userRepository.GetUserByNameAsync(loginDto.Username);
            if (user == null)
            {
                _logger.LogWarning("Login failed: User not found ({Username})", loginDto.Username);
                return null!;
            }

           
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                _logger.LogWarning("Login failed: Incorrect password for {Username}", loginDto.Username);
                return null!;
            }

         
            var token = _jwtHelper.GetJwtToken(user);

            var response = new AuthResponseDto
            {
                User = new UserResponseDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email
                },
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60) 
            };

            return response;
        }

        public async Task<AuthResponseDto> RegisterAsync(UserRegistrationDto registerDto)
        {

            bool isFirstUser = !await _userRepository.AnyUsersExistAsync();


            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            var role = isFirstUser ? "Admin" : "User";
            var newUser = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = hashedPassword,
                Role = "User"
            };

            await _userRepository.CreateUserAsync(newUser);


           
            var token = _jwtHelper.GetJwtToken(newUser);

            return new AuthResponseDto
            {
                User = new UserResponseDto
                {
                    Id = newUser.Id,
                    Username = newUser.Username,
                    Email = newUser.Email
                },
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60) 
            };
        }
    }
}
