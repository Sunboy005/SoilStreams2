﻿using SOILSTREAMAPI.Models.Dto;

namespace SOILSTREAMAPI.Models
{
    public class Order:BaseEntity
    {

        public string ConsumerUserId { get; set; }
        public virtual User ConsumerUser { get; set; }
        public List<SingleOrderDto> OrderedProducts { get; set; }
        public double TotalAmount { get; set; }
        public string DeliveryAddress { get; set; }
        public bool Status { get; set; }
        public bool DeliveryStatus { get; set; }
        public string TrackingId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpectedDeliveryDate { get; set; }

    }
}
