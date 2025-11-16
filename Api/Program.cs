using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();

#endregion