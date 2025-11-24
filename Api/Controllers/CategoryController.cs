using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CategoryController(IGenericService<Category> service) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategoriesAsync()
    {
        var categories = await service.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryAsync(int id)
    {
        var category = await service.GetByIdAsync(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategoryAsync(Category category)
    {
        var createdCategory = await service.CreateAsync(category);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> UpdateCategoryAsync(int id, Category category)
    {
        if (id != category.Id)
            return BadRequest();

        var updatedCategory = await service.UpdateAsync(category);
        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategoryAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}