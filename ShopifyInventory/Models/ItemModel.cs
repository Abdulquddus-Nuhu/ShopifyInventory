using System.ComponentModel.DataAnnotations;

namespace ShopifyInventory.Models
{
    public class ItemModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
        public DateTime DeletedAt { get; set; }

        [Required]
        public string DeletionComments { get; set; } = string.Empty;
    }
}
