using SOILSTREAMAPI.Models.Dto;

namespace SOILSTREAMAPI.Services.Interfaces
{
    public interface IStoreProductServices
    {
        Task<ResponseDto<StoreProductToReturnDto>> CreateStoreProduct(StoreProductCreationDto model);
        Task<ResponseDto<List<StoreProductToReturnDto>>> GetAllStoreProducts();
        Task<ResponseDto<List<StoreProductToReturnDto>>> GetStoreProductsByCategory(string categoryId);
        Task<ResponseDto<List<StoreProductToReturnDto>>> GetStoreProductsByUserId(string userId);
    }
}
