namespace Domain.Entities
{
    public class Category : AuditableEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
