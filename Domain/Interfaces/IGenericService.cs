using Domain.Entities;

namespace Domain.Interfaces;

public interface IGenericService<T> where T : BaseEntity
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<IEnumerable<T>> GetAllWithSpecificaitonsAsync(ISpecifications<T> specifications);
    public Task<T> GetByIdAsync(int entityId);
    public Task<T> GetByIdWithSpecificaitonsAsync(int entityId, ISpecifications<T> specifications);
    public Task<T> CreateAsync(T entity);
    public Task<bool> UpdateAsync(T entity);
    public Task<bool> DeleteAsync(int entityId);
}