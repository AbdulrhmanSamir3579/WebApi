using Api.Dtos.Products;
using AutoMapper;
using Domain.Entities;

namespace Api.Helpers;

public class ImageUrlResolver(IConfiguration configuration) : IValueResolver<Product, ProductSimpleReturnDto, string>
{
    public string Resolve(Product source, ProductSimpleReturnDto destination, string destMember,
        ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.ImageUrl))
            return $"{configuration["BaseUrl"]}/images/products/{source.ImageUrl}";
        return string.Empty;
    }
}