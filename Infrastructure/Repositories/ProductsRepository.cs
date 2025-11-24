using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProductsRepository(AppDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
{
    public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
    {
        return await _dbSet.Where(e => e.Name.ToLower().Contains(name)).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByBrandIdAsync(int brandId)
    {
        return await _dbSet.Where(e => e.BrandId == brandId).ToListAsync();
    }

    public async Task<List<IGrouping<int, Product>>> GetProductsGroupedByCategory(int categoryId)
    {
        return await _dbSet.GroupBy(p => p.CategoryId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(int categoryId)
    {
        return await _dbSet
            .Where(p => p.CategoryId == categoryId)
            .ToListAsync();
    }
}