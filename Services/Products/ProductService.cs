using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TinyFeetBackend.Data;
using TinyFeetBackend.DTOs.Products;
using TinyFeetBackend.Entities;
using TinyFeetBackend.Repositories.Interface;
using TinyFeetBackend.CloudinaryS;

namespace TinyFeetBackend.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly ICloudinaryService _cloudinary;
        private readonly AppDbContext _context;

        public ProductService(IProductRepository repository, IMapper mapper, ICloudinaryService cloudinary, AppDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _cloudinary = cloudinary;
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsAsync()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            return product == null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto?> CreateProductAsync(ProductCreateDto dto)
        {
            var product = _mapper.Map<Product>(dto);

            if (dto.Image != null)
            {
                var url = await _cloudinary.UploadImageAsync(dto.Image);
                product.ImageUrl = url;
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            var createdProduct = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == product.Id);
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task<ProductDto?> UpdateProductAsync(int id, ProductCreateDto dto)
        {
            var existing = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (existing == null) return null;

            _mapper.Map(dto, existing);

            if (dto.Image != null)
            {
                var url = await _cloudinary.UploadImageAsync(dto.Image);
                existing.ImageUrl = url;
            }

            await _context.SaveChangesAsync();

            var updatedProduct = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<ProductDto>(updatedProduct);
        }

        public async Task<ProductDto?> UpdateProductPriceAsync(int id, decimal price)
        {
            var product = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return null;

            product.Price = price;
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<ProductDto>?> GetProductBySearch(string search)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.Name.Contains(search) || p.Description.Contains(search))
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<IEnumerable<ProductDto>?> GetProductByCategoriesId(int id)
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.CategoryId == id)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }
    }
}