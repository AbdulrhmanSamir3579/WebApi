using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class CategoryController(ICategoryService categoryService) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetAllCategoriesAsync()
    {
        var categories = await categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Category>> GetCategoryAsync(int id)
    {
        var category = await categoryService.GetCategoryAsync(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategoryAsync(Category category)
    {
        var createdCategory = await categoryService.CreateCategoryAsync(category);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> UpdateCategoryAsync(int id, Category category)
    {
        if (id != category.Id)
            return BadRequest();
        
        var updatedCategory = await categoryService.UpdateCategoryAsync(category);
        return Ok(updatedCategory);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategoryAsync(int id)
    {
        await categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}