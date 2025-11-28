using Api.Errors;
using Api.Helpers;
using Application.Services;
using Domain.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
builder.Services.AddScoped<IProductRepository, ProductsRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddAutoMapper(cfg => { }, typeof(MappingProfiles));
builder.Services.AddDbContext<AppDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value.Errors.Count > 0)
            .SelectMany(x => x.Value.Errors)
            .Select(x => x.ErrorMessage)
            .ToArray();

        // Return a proper IActionResult
        return new BadRequestObjectResult(new ValidationErrorResponse()
        {
            Message = "Validation failed",
            Errors = errors
        });
    };
});



#endregion

var app = builder.Build();

#region Configure Database

using var scope = app.Services.CreateScope();
var servicesProvider = scope.ServiceProvider;
var dbContext = servicesProvider.GetRequiredService<AppDbContext>();
var loggerFactory = servicesProvider.GetRequiredService<ILoggerFactory>();
try
{
    await dbContext.Database.MigrateAsync();
}
catch (Exception e)
{
    loggerFactory.CreateLogger<Program>().LogError(e.Message);
    throw;
}

#endregion

#region Configure the HTTP request pipeline

if (app.Environment.IsDevelopment())
{
    app.MapControllers();
    app.MapOpenApi();
    app.UseAuthentication();
    app.UseAuthorization();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.Run();

#endregion