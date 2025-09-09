using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyFeetBackend.DTOs.Products;
using TinyFeetBackend.Services.Products;

namespace TinyFeetBackend.Controller
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productsService;

        public ProductController(IProductService productServices)
        {
            _productsService = productServices;
        }

        [HttpGet("View")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get()
        {
            var products = await _productsService.GetProductsAsync();
            return Ok(products);
        }

        [HttpGet("View/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productsService.GetByIdAsync(id);
            if (product == null) return NotFound("Product Not Found");
            return Ok(product);
        }

        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] string search)
        {
            var response = await _productsService.GetProductBySearch(search);
            return Ok(response);
        }

        [HttpGet("Category/{id}")]
        public async Task<IActionResult> GetByCategory(int id)
        {
            var products = await _productsService.GetProductByCategoriesId(id);
            if (products == null) return NotFound("Category Not Found");
            return Ok(products);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPost("create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Add([FromForm] ProductCreateDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = await _productsService.CreateProductAsync(product);
            return Ok(data);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("UpdatePrice/{id}")]
        public async Task<IActionResult> UpdatePrice(int id, [FromQuery] decimal price)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _productsService.UpdateProductPriceAsync(id, price);
            if (response == null) return BadRequest("Product Update Failed");
            return Ok(response);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpPut("update/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(int id, [FromForm] ProductCreateDto product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await _productsService.UpdateProductAsync(id, product);
            if (response == null) return BadRequest("Product Update Failed");
            return Ok(response);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("softDelete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deletedStatus = await _productsService.DeleteProductAsync(id);
            if (!deletedStatus) return BadRequest("Product Delete Failed");
            return Ok("Product deleted successfully");
        }
    }
}