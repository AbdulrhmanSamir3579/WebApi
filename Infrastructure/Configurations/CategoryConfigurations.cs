using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Configurations
{
    internal class CategoryConfigurations : IEntityTypeConfiguration<Category>
    {
        
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(100);

            builder.HasBaseType<AuditableEntity>();
        }
    }
}
