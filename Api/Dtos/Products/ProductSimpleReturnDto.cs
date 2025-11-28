using Domain.Entities;

namespace Api.Dtos.Products;

public class ProductSimpleReturnDto : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int BrandId { get; set; }
    public string? BrandName { get; set; } 
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
}