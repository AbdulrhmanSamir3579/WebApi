using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ProductService(IProductRepository repository) : IProductService
{
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<IEnumerable<Product>> GetAllByCategoryAsync(int categoryId)
    {
        return await repository.GetProductsByCategory(categoryId);
    }

    public async Task<IEnumerable<Product>> GetAllByBrandAsync(int brandId)
    {
        return await repository.GetProductsByBrandIdAsync(brandId);
    }

    public async Task<Product?> GetByIdAsync(int productId)
    {
        return await repository.GetByIdAsync(productId);
    }

    public async Task<Product> AddAsync(Product product)
    {
        return await repository.AddAsync(product);
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        return await repository.UpdateAsync(product);
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        return await repository.DeleteAsync(productId);
    }
}