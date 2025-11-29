using Api.Errors;
using Api.Helpers;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Extensions;

public static class ApplicationServicesExtension
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
        services.AddScoped<IProductRepository, ProductsRepository>();
        services.AddScoped<IProductService, ProductService>();
        services.AddAutoMapper(cfg => { }, typeof(MappingProfiles));
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var errors = actionContext.ModelState
                    .Where(e => e.Value?.Errors.Count > 0)
                    .SelectMany(e => e.Value!.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var response = new ErrorResponse(400, "Validation failed", errors);
                return new BadRequestObjectResult(response);
            };
        });
        return services;
    }
}