using SOILSTREAMAPI.Models.Dto;
using SOILSTREAMAPI.Services.Interfaces;

namespace SOILSTREAMAPI.Services.Implementations
{
    public class OrderServices : IOrderServices
    {
        Task<ResponseDto<OrderToReturnDto>> IOrderServices.CreateOrder(List<SingleOrderDto> orders)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<List<OrderToReturnDto>>> IOrderServices.GetAllOrders()
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<OrderToReturnDto>> IOrderServices.GetOrderById(string orderId)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<List<OrderToReturnDto>>> IOrderServices.GetOrderByUserId(string userId)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<OrderToReturnDto>> IOrderServices.UpdateOrderStatus(string orderId)
        {
            throw new NotImplementedException();
        }
    }
}
