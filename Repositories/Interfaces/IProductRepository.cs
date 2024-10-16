using Repositories.Entities;

namespace Repositories.Interfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<List<Product>> GetAllProducts();
    Task<Product> GetProductById(Guid productId);
    Task<bool> DeleteProductById(Guid productId);
    Task<bool> UpdateProduct(Product product);
}