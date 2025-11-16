using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    internal class SoftDeletableEntityConfigurations : IEntityTypeConfiguration<SoftDeletableEntity>
    {
        public void Configure(EntityTypeBuilder<SoftDeletableEntity> builder)
        {
            builder.Property(s => s.IsDeleted)
                   .IsRequired()
                   .HasDefaultValue(false);

            builder.Property(s => s.DeletedAt)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.DeletedBy)
                    .IsRequired(false)
                    .HasMaxLength(255);
        }
    }
}
