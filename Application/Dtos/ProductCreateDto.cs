namespace Application.Dtos;

public class ProductCreateDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int BrandId { get; set; }
    public int CategoryId { get; set; }
}