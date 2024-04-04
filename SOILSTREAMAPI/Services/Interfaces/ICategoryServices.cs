using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Models.Dto;

namespace SOILSTREAMAPI.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<ResponseDto<CategoryToReturn>> CreateCategory(CategoryToCreate model);
        Task<ResponseDto<CategoryToReturn>> GetCategoryByCategoryId(string id);
        Task<ResponseDto<CategoryToReturn>> GetCategoryByCategoryName(string name);
        Task<ProductCategory> GetCategoryById(string id);
        Task<ProductCategory> GetCategoryByName(string categoryName);
    }
}
