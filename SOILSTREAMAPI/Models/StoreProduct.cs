namespace SOILSTREAMAPI.Models
{
    public class StoreProduct:BaseEntity
    {
        public string UserId { get; set; }
        public virtual User User { get; set; }
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string StoreProductName { get; set; }
        public bool IsDeleted { get; set; }
        public int AvailableQuantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
