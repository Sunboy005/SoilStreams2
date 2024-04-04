using SOILSTREAMAPI.Models.Dto;

namespace SOILSTREAMAPI.Services.Interfaces
{
    public interface IOrderServices
    {
        Task<ResponseDto<OrderToReturnDto>> CreateOrder(List<SingleOrderDto> orders);
        Task<ResponseDto<List<OrderToReturnDto>>> GetAllOrders();
        Task<ResponseDto<OrderToReturnDto>> GetOrderById(string orderId);
        Task<ResponseDto<List<OrderToReturnDto>>> GetOrderByUserId(string userId);
        Task<ResponseDto<OrderToReturnDto>> UpdateOrderStatus(string orderId);
    }
}
