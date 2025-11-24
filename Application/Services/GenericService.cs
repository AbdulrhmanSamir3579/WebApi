using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class GenericService<T>(IGenericRepository<T> repository) : IGenericService<T> where T : BaseEntity
{
    public Task<IEnumerable<T>> GetAllAsync()
    {
        return repository.GetAllAsync();
    }

    public async Task<T> GetByIdAsync(int entityId)
    {
        return await repository.GetByIdAsync(entityId);
    }

    public Task<T> CreateAsync(T entity)
    {
        return repository.AddAsync(entity);
    }

    public Task<bool> UpdateAsync(T entity)
    {
        return repository.UpdateAsync(entity);
    }

    public Task<bool> DeleteAsync(int entityId)
    {
        return repository.DeleteAsync(entityId);
    }
}