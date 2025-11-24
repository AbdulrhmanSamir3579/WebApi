using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
    {
        return await categoryRepository.GetAllAsync();
    }

    public async Task<Category> GetCategoryAsync(int categoryId)
    {
        var category = await categoryRepository.GetByIdAsync(categoryId);
        return category;
    }

    public Task<Category> CreateCategoryAsync(Category category)
    {
        return categoryRepository.AddAsync(category);
    }

    public Task<bool> UpdateCategoryAsync(Category category)
    {
        return categoryRepository.UpdateAsync(category);
    }

    public Task<bool> DeleteCategoryAsync(int categoryId)
    {
        return categoryRepository.DeleteAsync(categoryId);
    }
}