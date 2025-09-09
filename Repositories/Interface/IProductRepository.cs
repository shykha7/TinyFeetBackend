using TinyFeetBackend.Entities;

namespace TinyFeetBackend.Repositories.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>?> GetByCategoryId(int id);
        Task<Product?> GetByProductId(int id);
    }
}
