namespace SOILSTREAMAPI.Models.Dto
{
    public class StoreProductToReturnDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategory { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
