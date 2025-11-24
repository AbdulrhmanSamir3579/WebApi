using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class GenericRepository<T>(AppDbContext dbContext) : IGenericRepository<T>
    where T : BaseEntity
{
    protected readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.AsNoTracking().ToListAsync();
    }

    public async Task<IEnumerable<T>> GetAllWithSpecsAsync(ISpecifications<T> specifications)
    {
        return await ApplySpecifications(specifications).ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T?> GetByIdWithSpecsAsync(int id, ISpecifications<T> specifications)
    {
        return await ApplySpecifications(specifications).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        var existingEntity = await _dbSet.FindAsync(entity.Id);
        if (existingEntity is null)
            return false;
        dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existingEntity = await _dbSet.FindAsync(id);
        if (existingEntity is null)
            return false;
        _dbSet.Remove(existingEntity);
        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    private IQueryable<T> ApplySpecifications(ISpecifications<T> specifications)
    {
        return SpecificationEvaluator<T>.GetQuery(_dbSet.AsQueryable(), specifications);
    }
}