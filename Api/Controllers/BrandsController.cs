using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class BrandsController(IGenericService<Brand> service) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Brand>>> GetAllBrandsAsync()
    {
        var brands = await service.GetAllAsync();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Brand>> GetBrandAsync(int id)
    {
        var brand = await service.GetByIdAsync(id);
        return Ok(brand);
    }

    [HttpPost]
    public async Task<ActionResult<Brand>> CreateBrandAsync(Brand brand)
    {
        await service.CreateAsync(brand);
        return Created();
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Brand>> UpdateBrandAsync(int id, Brand brand)
    {
        if (id != brand.Id)
            return BadRequest();

        var updatedBrand = await service.UpdateAsync(brand);
        return Ok(updatedBrand);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteBrandAsync(int id)
    {
        await service.DeleteAsync(id);
        return NoContent();
    }
}