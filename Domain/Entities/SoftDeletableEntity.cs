namespace Domain.Entities
{
    public class SoftDeletableEntity : AuditableEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; } = DateTime.UtcNow;
        public string? DeletedBy { get; set; } = null!;
    }
}