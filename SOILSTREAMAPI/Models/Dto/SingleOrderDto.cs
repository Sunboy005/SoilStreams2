namespace SOILSTREAMAPI.Models.Dto
{
    public class SingleOrderDto:BaseEntity
    {
        public int StoreProductId { get; set; }
        public virtual StoreProduct StoreProduct { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
