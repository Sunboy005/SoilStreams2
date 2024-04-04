namespace SOILSTREAMAPI.Models.Dto
{
    public class ProductToReturnDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ProductCategory Category { get; set; }
        public string Description { get; set; }
    }
}
