using Domain.Entities;

namespace Domain.Interfaces;

public interface IGenericRepository<T> where T : BaseEntity
{
    public Task<IEnumerable<T>> GetAllAsync();
    public Task<IEnumerable<T>> GetAllWithSpecsAsync(ISpecifications<T> specifications);
    public Task<T?> GetByIdAsync(int id);
    public Task<T?> GetByIdWithSpecsAsync(int id, ISpecifications<T> specifications);
    public Task<T> AddAsync(T entity);
    public Task<bool> UpdateAsync(T entity);
    public Task<bool> DeleteAsync(int id);
    public Task<int> SaveChangesAsync();
}