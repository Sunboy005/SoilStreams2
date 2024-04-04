using SOILSTREAMAPI.Models.Dto;
using SOILSTREAMAPI.Services.Interfaces;

namespace SOILSTREAMAPI.Services.Implementations
{
    public class StoreProductServices : IStoreProductServices
    {
        Task<ResponseDto<StoreProductToReturnDto>> IStoreProductServices.CreateStoreProduct(StoreProductCreationDto model)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<StoreProductToReturnDto>> IStoreProductServices.GetAllStoreProducts()
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<StoreProductToReturnDto>> IStoreProductServices.GetStoreProductsByCategory(string category)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<StoreProductToReturnDto>> IStoreProductServices.GetStoreProductsByEmail(string email)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<StoreProductToReturnDto>> IStoreProductServices.GetStoreProductsByUserId(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
