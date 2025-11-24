using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductService
{
    public Task<IEnumerable<Product>> GetAllProductsAsync();
    public Task<IEnumerable<Product>> GetAllProductsByCategoryAsync(int categoryId);
    public Task<IEnumerable<Product>> GetAllProductsByBrandAsync(int brandId);
    public Task<Product> GetProductByIdAsync(int productId);
    public Task<Product> AddProductAsync(Product product);
    public Task<bool> UpdateProductAsync(Product product);
    public Task<bool> DeleteProductAsync(int productId);
}