using Domain.Entities;
using Domain.Interfaces;
using Domain.Specifications;
using Domain.Specifications.Products;

namespace Application.Services;

public class ProductService(IGenericRepository<Product> repository)
    : GenericService<Product>(repository), IProductService
{
}