using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class ProductService : IProductService
{
    public Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllProductsByCategoryAsync(int categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Product>> GetAllProductsByBrandAsync(int brandId)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(int productId)
    {
        throw new NotImplementedException();
    }

    public Task<Product> AddProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProductAsync(int productId)
    {
        throw new NotImplementedException();
    }
}