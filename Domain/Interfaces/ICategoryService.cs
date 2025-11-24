namespace Domain.Interfaces;

using Domain.Entities;

public interface ICategoryService
{
    public Task<IEnumerable<Category>> GetAllCategoriesAsync();
    public Task<Category> GetCategoryAsync(int categoryId);
    public Task<Category> CreateCategoryAsync(Category category);
    public Task<bool> UpdateCategoryAsync(Category category);
    public Task<bool> DeleteCategoryAsync(int categoryId);
}