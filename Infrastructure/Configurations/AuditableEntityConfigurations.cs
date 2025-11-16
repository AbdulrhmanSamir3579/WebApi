using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Configurations
{
    internal class AuditableEntityConfigurations : IEntityTypeConfiguration<AuditableEntity>
    {
        public void Configure(EntityTypeBuilder<AuditableEntity> builder)
        {
            builder.Property(a => a.CreatedAt)
                   .IsRequired().HasDefaultValueSql("GETUTCDATE()");
            builder.Property(a => a.CreatedBy)
                  .IsRequired()
                  .HasMaxLength(100);

            builder.Property(a => a.ModifiedAt)
                   .IsRequired(false);

            builder.Property(a => a.ModifiedBy)
                   .HasMaxLength(100)
                   .IsRequired(false);

        }
    }
}
