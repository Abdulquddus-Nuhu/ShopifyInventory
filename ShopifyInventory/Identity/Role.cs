using Microsoft.AspNetCore.Identity;

namespace ShopifyInventory.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public Role()
        {
        }
        public Role(string roleName)
        {
            Name = roleName;
        }
    }
}
