using System.Reflection;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Table-Per-Type (TPT) inheritance strategy
        // modelBuilder.Entity<AuditableEntity>()
        //     .UseTptMappingStrategy();

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        // {
        //     if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
        //     {
        //         modelBuilder.Entity(entityType.ClrType)
        //             .Property<DateTime>("CreatedAt")
        //             .HasDefaultValueSql("GETUTCDATE()");
        //
        //         modelBuilder.Entity(entityType.ClrType)
        //             .Property<string>("CreatedBy")
        //             .IsRequired()
        //             .HasMaxLength(100);
        //     }
        //
        //     if (typeof(SoftDeletableEntity).IsAssignableFrom(entityType.ClrType))
        //     {
        //         modelBuilder.Entity(entityType.ClrType)
        //             .Property<bool>("IsDeleted")
        //             .HasDefaultValue(false);
        //         modelBuilder.Entity(entityType.ClrType)
        //             .Property<string>("DeletedBy")
        //             .HasMaxLength(255)
        //             .IsRequired(false);
        //         modelBuilder.Entity(entityType.ClrType)
        //             .Property<DateTime?>("DeletedAt")
        //             .IsRequired(false);
        //     }
        // }
        //
        // base.OnModelCreating(modelBuilder);
    }

    
    public DbSet<Product>  Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Brand>  Brands { get; set; }
}