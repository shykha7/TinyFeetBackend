using Microsoft.AspNetCore.Mvc;
using TinyFeetBackend.CloudinaryS;

namespace TinyFeetBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CloudinaryController : ControllerBase
    {
        private readonly ICloudinaryService _cloudinaryService;

        public CloudinaryController(ICloudinaryService cloudinaryService)
        {
            _cloudinaryService = cloudinaryService;
        }

   


        [HttpPost("image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            var url = await _cloudinaryService.UploadImageAsync(file);
            if (url == null) return BadRequest("Upload failed");
            return Ok(new { Url = url });
        }

    }
}
