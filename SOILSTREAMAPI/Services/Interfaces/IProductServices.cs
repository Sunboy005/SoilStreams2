using SOILSTREAMAPI.Models.Dto;

namespace SOILSTREAMAPI.Services.Interfaces
{
    public interface IProductServices
    {
        Task<ResponseDto<ProductToReturnDto>> CreateProduct(ProductCreationDto model);
        Task<ResponseDto<ProductToReturnDto>> GetAllProducts();
        Task<ResponseDto<ProductToReturnDto>> GetProductById( string Id);
    }
}
