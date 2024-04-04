using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Models.Dto;

namespace SOILSTREAMAPI.Services.Interfaces
{
    public interface IProductServices
    {
        Task<ResponseDto<ProductToReturnDto>> GetProductByProductId(string Id);
        Task<ResponseDto<ProductToReturnDto>> GetProductByProductName(string name);
        Task<ResponseDto<List<ProductToReturnDto>>> GetProductsByProductCategoryId(string categoryId);
        Task<ResponseDto<List<ProductToReturnDto>>> GetProductsByProductCategoryName(string categoryName);
        Task<Product> GetProductById(string id);
        Task<Product> GetProductByName(string name);
        Task<ResponseDto<ProductToReturnDto>> CreateProduct(ProductCreationDto model);
        Task<ResponseDto<List<ProductToReturnDto>>> GetAllProducts();
    }
}
