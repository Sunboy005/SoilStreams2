using SOILSTREAMAPI.Models.Dto;

namespace SOILSTREAMAPI.Services.Interfaces
{
    public interface IStoreProductServices
    {
        Task<ResponseDto<StoreProductToReturnDto>> CreateStoreProduct(StoreProductCreationDto model);
        Task<ResponseDto<StoreProductToReturnDto>> GetAllStoreProducts();
        Task<ResponseDto<StoreProductToReturnDto>> GetStoreProductsByUserId(string userId);
        Task<ResponseDto<StoreProductToReturnDto>> GetStoreProductsByEmail(string email);
        Task<ResponseDto<StoreProductToReturnDto>> GetStoreProductsByCategory(string category);

    }
}
