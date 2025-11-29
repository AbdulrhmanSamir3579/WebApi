using Api.Extensions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

builder.Services.AddOpenApi();
builder.Services.AddControllers();
builder.Services.AddApplicationService();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

#endregion

var app = builder.Build();

#region Configure Database

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<AppDbContext>();
    var logger = services.GetRequiredService<ILogger<Program>>();

    try
    {
        await dbContext.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error while migrating database");
        throw;
    }
}

#endregion

#region Configure the HTTP request pipeline

// 🔥 1. Global exception middleware MUST be first
app.UseApiExceptionHandling();

// 2. Development tools
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// 3. HTTPS redirection
app.UseHttpsRedirection();

// 4. Static files
app.UseStaticFiles();

// 5. Authentication & Authorization → should NOT be inside development block
app.UseAuthentication();
app.UseAuthorization();

// 6. Routing
app.MapControllers();

app.Run();

#endregion