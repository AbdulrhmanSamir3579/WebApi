using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class GenericService<T>(IGenericRepository<T> repository) : IGenericService<T> where T : BaseEntity
{
    public async Task<IEnumerable<T>> GetAllWithSpecificaitonsAsync(ISpecifications<T> specifications)
    {
        return await repository.GetAllWithSpecsAsync(specifications);
    }

    public async Task<T> GetByIdAsync(int entityId)
    {
        return await repository.GetByIdAsync(entityId);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await repository.GetAllAsync();
    }

    public async Task<T> GetByIdWithSpecificaitonsAsync(int entityId, ISpecifications<T> specifications)
    {
        return await repository.GetByIdWithSpecsAsync(entityId, specifications);
    }

    public async Task<T> GetByIdWithSpecificationsAsync(int entityId, ISpecifications<T> specifications)
    {
        return await repository.GetByIdWithSpecsAsync(entityId, specifications);
    }

    public async Task<T> CreateAsync(T entity)
    {
        return await repository.AddAsync(entity);
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        return await repository.UpdateAsync(entity);
    }

    public async Task<bool> DeleteAsync(int entityId)
    {
        return await repository.DeleteAsync(entityId);
    }
}