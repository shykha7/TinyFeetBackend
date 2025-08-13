using AutoMapper;
using TinyFeetBackend.DTOs.Products;
using TinyFeetBackend.Entities;
using TinyFeetBackend.Repositories.Interface;

namespace TinyFeetBackend.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> GetAllProductsAsync(int? categoryId = null)
        {
            var products = await _repository.GetAllAsync();
            return _mapper.Map<List<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);
            var created = await _repository.CreateAsync(product);
            return _mapper.Map<ProductDto>(created);
        }

        public async Task<bool> UpdateProductAsync(int id, ProductCreateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            _mapper.Map(dto, existing);
            await _repository.UpdateAsync(existing);
            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) return false;

            await _repository.DeleteAsync(existing);
            return true;
        }



    }
}
