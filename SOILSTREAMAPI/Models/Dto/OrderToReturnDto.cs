namespace SOILSTREAMAPI.Models.Dto
{
    public class OrderToReturnDto
    {
            public string OrderId { get; set; }
            public string TrackingId { get; set; }
            public string OrderBy { get; set; }
            public string DeliveryAddress { get; set; }
            public DateTime ExpectedDeliveryDate { get; set; }
            public DateTime OrderDate { get; set; }
        }
    }

