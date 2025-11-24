using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductService 
{
    public Task<IEnumerable<Product>> GetAllAsync();
    public Task<IEnumerable<Product>> GetAllByCategoryAsync(int categoryId);
    public Task<IEnumerable<Product>> GetAllByBrandAsync(int brandId);
    public Task<Product?> GetByIdAsync(int productId);
    public Task<Product> AddAsync(Product product);
    public Task<bool> UpdateProductAsync(Product product);
    public Task<bool> DeleteProductAsync(int productId);
}