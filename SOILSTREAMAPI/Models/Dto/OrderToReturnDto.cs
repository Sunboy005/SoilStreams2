namespace SOILSTREAMAPI.Models.Dto
{
    public class OrderToReturnDto
    {
            public int Id { get; set; }
            public string ConsumerUserId { get; set; }
            public int StoreId { get; set; }
            public List<SingleOrderDto> OrderedItems { get; set; }
            public DateTime OrderDate { get; set; }
        }
    }

