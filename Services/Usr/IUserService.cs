using TinyFeetBackend.Entities;

namespace TinyFeetBackend.Services.Usr
{
    public interface IUserService
    {

        Task<IEnumerable<object>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<bool> ToggleBlockUserAsync(int id);
    }
}
