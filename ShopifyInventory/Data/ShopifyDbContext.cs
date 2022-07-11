using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopifyInventory.Data.Entities;
using ShopifyInventory.Identity;
using ShopifyInventory.Models;

namespace ShopifyInventory.Data
{
    public class ShopifyDbContext : IdentityDbContext<Persona, Role, Guid>
    {
        private readonly DbContextOptions _options;
        public ShopifyDbContext(DbContextOptions<ShopifyDbContext> options) : base(options)
        {
            _options = options;
        }

        public DbSet<Item>? Items { get; set; }
        public DbSet<ItemModel>? ItemModel { get; set; }

    }
}
