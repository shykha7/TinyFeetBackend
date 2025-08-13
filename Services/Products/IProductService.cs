using TinyFeetBackend.DTOs.Products;

namespace TinyFeetBackend.Services.Products
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetAllProductsAsync( int? categoryId = null);
        Task<ProductDto?> GetProductByIdAsync(int id);
        Task<ProductDto> CreateProductAsync(ProductCreateDto dto);
        Task<bool> UpdateProductAsync(int id, ProductCreateDto dto);
        Task<bool> DeleteProductAsync(int id);
    }
}
