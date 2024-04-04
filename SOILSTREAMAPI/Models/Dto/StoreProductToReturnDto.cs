namespace SOILSTREAMAPI.Models.Dto
{
    public class StoreProductToReturnDto
    {
        public string Id { get; set; }
        public string IsDeleted { get; set; }
        public Product Product { get; set; }
        public string StoreProductName { get; set; }
        public ProductCategory Category { get; set; }
        public int AvailableQuantity { get; set; }
        public double UnitPrice { get; set; }
    }
}
