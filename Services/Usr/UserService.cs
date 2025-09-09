using TinyFeetBackend.Entities;
using TinyFeetBackend.Repositories.Interface;

namespace TinyFeetBackend.Services.Usr
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<object>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }


        public async Task<bool> ToggleBlockUserAsync(int id)
        {
            return await _userRepository.ToggleBlockUserAsync(id);
        }


    }
}

