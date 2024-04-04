using Microsoft.EntityFrameworkCore;
using SOILSTREAMAPI.Data;
using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Models.Dto;
using SOILSTREAMAPI.Services.Interfaces;

namespace SOILSTREAMAPI.Services.Implementations
{
    public class CategoryServices : ICategoryServices
    {
        private readonly SoilStreamsDbContext _dbContext;
        public CategoryServices(SoilStreamsDbContext dbContext)
        {
             _dbContext= dbContext;
        }
        public async Task<ResponseDto<CategoryToReturn>> CreateCategory(CategoryToCreate model)
        {
            var category = new ProductCategory()
            {
                Name = model.Name,
            };
            _dbContext.ProductCategories.Add(category);
            var saveChanges=_dbContext.SaveChanges();
            if (saveChanges > 0)
            {
                return new ResponseDto<CategoryToReturn>()
                {
                     IsSuccessful = true,
                      StatusCode="00",
                       StatusMessage = "Product Category saved"
                };
            }
            return new ResponseDto<CategoryToReturn>()
            {
                IsSuccessful = false,
                StatusCode = "08",
                StatusMessage = "Product Category failed to save. Please try again"
            };
        }


        public async Task<ResponseDto<CategoryToReturn>> GetCategoryByCategoryName(string name)
        {
            var category = await GetCategoryByName(name);
            if (category != null)
            {
                var categoryToReturn = new CategoryToReturn()
                {
                    Name = category.Name,
                    Id = category.Id
                };
                return new ResponseDto<CategoryToReturn>()
                {
                    Data = categoryToReturn,
                    IsSuccessful = true,
                    StatusCode = "00",
                    StatusMessage = "Product category retrieved"

                };
            }
            return new ResponseDto<CategoryToReturn>()
            {
                IsSuccessful = false,
                StatusCode = "14",
                StatusMessage = "Product category not found"

            };
        }
        

        public async Task<ResponseDto<CategoryToReturn>> GetCategoryByCategoryId(string id)
        {
            var category = await GetCategoryById(id);
            if (category != null)
            {
                var categoryToReturn = new CategoryToReturn()
                {
                    Name = category.Name,
                    Id = category.Id
                };
                return new ResponseDto<CategoryToReturn>()
                {
                    Data = categoryToReturn,
                    IsSuccessful = true,
                    StatusCode = "00",
                    StatusMessage = "Product category retrieved"

                };
            }
            return new ResponseDto<CategoryToReturn>()
            {
                IsSuccessful = false,
                StatusCode = "14",
                StatusMessage = "Product category not found"

            };
        }
        public async Task<ProductCategory> GetCategoryById(string id)
        {
            var category = await _dbContext.ProductCategories.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (category != null)
            {
                return category;
            }
            return null;
        }

        public async Task<ProductCategory> GetCategoryByName(string categoryName)
        {
            var category = await _dbContext.ProductCategories.Where(c => c.Name == categoryName).FirstOrDefaultAsync();
            if (category != null)
            {
                return category;
            }
            return null;
        }
    }
}
