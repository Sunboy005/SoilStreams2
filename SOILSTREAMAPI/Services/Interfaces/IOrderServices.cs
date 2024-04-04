using SOILSTREAMAPI.Models.Dto;

namespace SOILSTREAMAPI.Services.Interfaces
{
    public interface IOrderServices
    {
        Task<ResponseDto<OrderToReturnDto>> CreateOrder(OrderToCreate model);
        Task<ResponseDto<List<OrderToReturnDto>>> GetAllOrders();
        Task<OrderToReturnDto> GetOrderByTrackingId(string trackingId);
        Task<OrderToReturnDto> GetOrderByOrderId(string orderId);
        Task<ResponseDto<List<OrderToReturnDto>>> GetOrderByUserId(string userId);
        Task<ResponseDto<OrderToReturnDto>> UpdateOrderStatus(OrderToUpdate model);
    }
}
