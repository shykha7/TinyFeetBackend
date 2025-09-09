using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TinyFeetBackend.Data;

namespace TinyFeetBackend.Controller
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("All")]
        public async Task<ActionResult> GetAll()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories.Select(c => new { id = c.Id, name = c.Name }));
        }
    }
}