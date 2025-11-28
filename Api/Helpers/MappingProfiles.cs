using Api.Dtos.Products;
using AutoMapper;
using Domain.Entities;

namespace Api.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductSimpleReturnDto>()
            .ForMember(d => d.BrandName, opt => opt.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category.Name))
            .ForMember(d => d.ImageUrl, opt => opt.MapFrom<ImageUrlResolver>());
    }
}