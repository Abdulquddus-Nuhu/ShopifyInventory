using Microsoft.AspNetCore.Identity;

namespace ShopifyInventory.Identity
{
    public class Persona : IdentityUser<Guid>
    {
        public bool IsDeleted { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual string? LastModifyBy { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public Persona()
        {
            IsDeleted = false;
            Created = DateTime.UtcNow;
        }
    }
}
