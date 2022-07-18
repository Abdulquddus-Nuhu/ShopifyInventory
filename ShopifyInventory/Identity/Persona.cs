using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ShopifyInventory.Identity
{
    public class Persona : IdentityUser<Guid>
    {
        public bool IsDeleted { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual string? LastModifyBy { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        public Persona()
        {
            IsDeleted = false;
            Created = DateTime.UtcNow;
        }
    }
}
