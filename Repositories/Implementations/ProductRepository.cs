using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TinyFeetBackend.Data;
using TinyFeetBackend.Entities;
using TinyFeetBackend.Repositories.Interface;

namespace TinyFeetBackend.Repositories.Implementations
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        public ProductRepository(AppDbContext vagueVaultDbContext, IMapper mapper)
        {
            _dbContext = vagueVaultDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Product>?> GetByCategoryId(int id)
        {
            var result = await _dbContext.Products.Where(p => p.CategoryId == id).ToListAsync();
            if (result == null) return null;

            return result;
        }

        public async Task<Product?> GetByProductId(int id)
        {
            var data = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null) return null;
            return data;

        }

    }
}
