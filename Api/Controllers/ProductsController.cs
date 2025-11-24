using Application.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications.Products;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProductsController(IProductService productService) : BaseApiController
{
    // GET: api/products
    // Optional filters: ?categoryId=5&brandId=3
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll([FromQuery] int? categoryId, [FromQuery] int? brandId)
    {
        ISpecifications<Product> specs = new ProductsWithCategoryAndBrandSpec(categoryId, brandId);
        IEnumerable<Product> products = await productService.GetAllWithSpecificaitonsAsync(specs);
        return Ok(products);
    }

    // GET: api/products/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetById(int id)
    {
        var product = await productService.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<Product>> Post([FromBody] ProductCreateDto dto)
    {
        var productDto = new Product()
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            BrandId = dto.BrandId,
            CategoryId = dto.CategoryId
        };
        var createdProduct = await productService.CreateAsync(productDto);
        return Ok(createdProduct);
    }


    // PUT: api/products/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] Product product)
    {
        if (product == null || product.Id != id)
            return BadRequest();

        var updated = await productService.UpdateAsync(product);

        if (!updated)
            return NotFound(); // 404 if the product doesn't exist

        return NoContent(); // 204 No Content
    }

    // DELETE: api/products/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await productService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}