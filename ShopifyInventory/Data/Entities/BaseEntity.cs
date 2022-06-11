using System.ComponentModel.DataAnnotations;

namespace ShopifyInventory.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime? Created { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DeletedAt { get; set; }
        public string? DeletionComments { get; set; } = string.Empty;

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            IsDeleted = false;
            Created = DateTime.UtcNow;
        }
    }
}
