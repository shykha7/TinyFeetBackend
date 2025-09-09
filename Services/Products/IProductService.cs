using TinyFeetBackend.DTOs.Products;

namespace TinyFeetBackend.Services.Products
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProductsAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto?> CreateProductAsync(ProductCreateDto product);
        Task<ProductDto?> UpdateProductAsync(int id, ProductCreateDto product);
        Task<ProductDto?> UpdateProductPriceAsync(int id, decimal price);
        Task<bool> DeleteProductAsync(int id);
        Task<IEnumerable<ProductDto>?> GetProductBySearch(string search);
        Task<IEnumerable<ProductDto>?> GetProductByCategoriesId(int id);
    }
}