using System;
using System.Linq.Expressions;
using Domain.Entities;

namespace Domain.Specifications.Products
{
    public class ProductsWithCategoryAndBrandSpec : BaseSpecfications<Product>
    {
        public ProductsWithCategoryAndBrandSpec(int? categoryId = null, int? brandId = null)
        {
            Criteria = p =>
                (!categoryId.HasValue || p.CategoryId == categoryId.Value) &&
                (!brandId.HasValue || p.BrandId == brandId.Value);

            Includes.Add(p => p.Brand);
            Includes.Add(p => p.Category);
        }
    }
}