using Api.Dtos.Products;
using Application.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Specifications.Products;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class ProductsController(IProductService productService, IMapper mapper) : BaseApiController
{
    // GET: api/products
    // Optional filters: ?categoryId=5&brandId=3
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductSimpleReturnDto>>> GetAll(
        [FromQuery] int? categoryId, 
        [FromQuery] int? brandId)
    {
        ISpecifications<Product> specs = new ProductsWithCategoryAndBrandSpec(categoryId, brandId);
        IEnumerable<Product> products = await productService.GetAllWithSpecificaitonsAsync(specs);
        return Ok(mapper.Map<IEnumerable<Product>, IEnumerable<ProductSimpleReturnDto>>(products));
    }

    // GET: api/products/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProductSimpleReturnDto>> GetById(int id)
    {
        var product = await productService.GetByIdAsync(id);

        if (product == null)
            throw new NotFoundException("Product", id);

        return Ok(mapper.Map<Product, ProductSimpleReturnDto>(product));
    }

    // POST: api/products
    [HttpPost]
    public async Task<ActionResult<ProductSimpleReturnDto>> Post([FromBody] ProductCreateDto dto)
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
        var returnDto = mapper.Map<Product, ProductSimpleReturnDto>(createdProduct);
        
        return CreatedAtAction(nameof(GetById), new { id = createdProduct.Id }, returnDto);
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
        var product = await productService.GetByIdAsync(id);
        
        if (product == null)
            throw new NotFoundException("Product", id);

        await productService.DeleteAsync(id);

        return NoContent();
    }
}