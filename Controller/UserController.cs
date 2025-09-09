using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyFeetBackend.Services.Usr;

namespace TinyFeetBackend.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/users
        [HttpGet("All")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound(new { Message = "User not found" });

            return Ok(user);
        }

        // PATCH: api/users/{id}/block
        [HttpPatch("{id}/block")]
        public async Task<IActionResult> ToggleBlockUserAsync(int id)
        {
            var result = await _userService.ToggleBlockUserAsync(id);
            if (!result)
                return NotFound(new { Message = "User not found" });

           
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(new
            {
                user.Id,
                user.Username,
                user.IsBlocked,
                Message = user.IsBlocked ? "User blocked" : "User unblocked"
            });
        }
    }
}
