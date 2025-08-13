
using Microsoft.EntityFrameworkCore;
using TinyFeetBackend.Data;
using TinyFeetBackend.Entities;
using TinyFeetBackend.Helpers.Interfaces;
using TinyFeetBackend.Repositories.Interface;

namespace TinyFeetBackend.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
       

        public UserRepository(AppDbContext appDbContext, IPasswordHasher _passwordhasher)
        {
            _context = appDbContext;
          
        }
        public async Task<User?> GetUserByNameAsync(string username)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);
           
            return user;
        } 


        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _context.Users.
                AnyAsync(u => u.Username == username || u.Email == email);
        }

        public async Task CreateUserAsync(User user)
        {
             _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

        }

        public async Task<bool> AnyUsersExistAsync()
        {
            return await _context.Users.AnyAsync();
        }

    }
}


