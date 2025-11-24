using Application.Dtos;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProductsController(IProductService productService) : BaseApiController
{
    // GET: api/products
    // Optional filters: ?categoryId=5&brandId=3
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll([FromQuery] int? categoryId, [FromQuery] int? brandId)
    {
        IEnumerable<Product> products;

        if (categoryId.HasValue)
        {
            products = await productService.GetAllByCategoryAsync(categoryId.Value);
        }
        else if (brandId.HasValue)
        {
            products = await productService.GetAllByBrandAsync(brandId.Value);
        }
        else
        {
            products = await productService.GetAllAsync();
        }

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
    public async Task<ActionResult<Product>> Create(ProductCreateDto dto)
    {
        var productDto = new Product
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            BrandId = dto.BrandId,
            CategoryId = dto.CategoryId
        };

        if (productDto == null)
            return BadRequest();

        var createdProduct = await productService.AddAsync(productDto);
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, createdProduct);
    }

    // PUT: api/products/5
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] Product product)
    {
        if (product == null || product.Id != id)
            return BadRequest();

        var updated = await productService.UpdateProductAsync(product);

        if (!updated)
            return NotFound(); // 404 if the product doesn't exist

        return NoContent(); // 204 No Content
    }

    // DELETE: api/products/5
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await productService.DeleteProductAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}