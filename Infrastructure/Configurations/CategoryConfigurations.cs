using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configurations
{
    internal class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            
            builder.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            builder.HasIndex(c => c.Name).IsUnique();

        }
    }
}
