using TinyFeetBackend.Entities;

namespace TinyFeetBackend.Repositories.Interface
{
    public interface IUserRepository
    {

        Task<User?> GetUserByNameAsync(string Username);
        Task<bool> UserExistsAsync(string username, string email);
        Task CreateUserAsync(User user);
        Task<bool> AnyUsersExistAsync();
    }
}
