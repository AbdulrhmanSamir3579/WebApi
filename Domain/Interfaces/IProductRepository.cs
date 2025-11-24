using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductRepository : IGenericRepository<Product>
{
    public Task<IEnumerable<Product>> GetProductsByNameAsync(string name);
    public Task<IEnumerable<Product>> GetProductsByBrandIdAsync(int brandId);
    public Task<List<IGrouping<int, Product>>> GetProductsGroupedByCategory(int categoryId);
    public Task<IEnumerable<Product>> GetProductsByCategory(int categoryId);
}