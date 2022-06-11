namespace ShopifyInventory.Data.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public int Quantity { get; set; } 
        public string Description { get; set; } = string.Empty;
    }
}
