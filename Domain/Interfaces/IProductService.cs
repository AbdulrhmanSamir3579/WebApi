using Domain.Entities;

namespace Domain.Interfaces;

public interface IProductService
{
    public Task<IReadOnlyList<Product>> GetAllProductsAsync();
    public Task<IReadOnlyList<Product>> GetAllProductsByCategoryAsync(int categoryId);
    public Task<IReadOnlyList<Product>> GetAllProductsByBrandAsync(int brandId);
    public Task<Product> GetProductByIdAsync(int productId);
    public Task<Product> AddProductAsync(Product product);
    public Task<bool> UpdateProductAsync(Product product);
    public Task<bool> DeleteProductAsync(int productId);
}