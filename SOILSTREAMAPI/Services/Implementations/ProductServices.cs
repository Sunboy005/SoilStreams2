using SOILSTREAMAPI.Models.Dto;
using SOILSTREAMAPI.Services.Interfaces;

namespace SOILSTREAMAPI.Services.Implementations
{
    public class ProductServices : IProductServices
    {
        Task<ResponseDto<ProductToReturnDto>> IProductServices.CreateProduct(ProductCreationDto model)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<ProductToReturnDto>> IProductServices.GetAllProducts()
        {
            throw new NotImplementedException();
        }

        Task<ResponseDto<ProductToReturnDto>> IProductServices.GetProductById(string Id)
        {
            throw new NotImplementedException();
        }
    }
}
