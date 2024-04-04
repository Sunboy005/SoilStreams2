namespace SOILSTREAMAPI.Models.Dto
{
    public class OrderToUpdate
    {
        public string OrderId { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Status { get; set; }
        public bool DeliveryStatus { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }
    }
}
