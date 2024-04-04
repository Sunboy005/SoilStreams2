namespace SOILSTREAMAPI.Models.Dto
{
    public class OrderToCreate
    {
        public List<SingleOrderDto> OrderItems { get; set; }
        public double TotalAmount { get; set; }
        public string DeliveryAddress { get; set; }
        public string OrderBy { get; set; }

    }
}
