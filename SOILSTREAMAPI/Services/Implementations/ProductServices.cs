using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SOILSTREAMAPI.Data;
using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Models.Dto;
using SOILSTREAMAPI.Services.Interfaces;
using System.Xml.Linq;

namespace SOILSTREAMAPI.Services.Implementations
{
    public class ProductServices : IProductServices
    {
        private readonly SoilStreamsDbContext _dbContext;
        private readonly ICategoryServices _categoryServices;
        public ProductServices(ICategoryServices categoryServices, SoilStreamsDbContext dbContext)
        {
             _dbContext= dbContext;
            _categoryServices= categoryServices;
        }
        public async Task<ResponseDto<ProductToReturnDto>> CreateProduct(ProductCreationDto model)
        {
            var category=await _categoryServices.GetCategoryById(model.CategoryId);
            if (category == null)
            {
                return new ResponseDto<ProductToReturnDto>()
                {
                    StatusCode = "08",
                    StatusMessage = "Invalid category",
                    IsSuccessful = false,
                };
            }
            var product = new Product()
            {
                Name = model.Name,
                 Category=category,
                  DateCreated=DateTime.Now,
                   DateModified=DateTime.Now,
                    Description=model.Description                     
            };
            _dbContext.Products.Add(product);
            var saveChanges = _dbContext.SaveChanges();
            if (saveChanges > 0)
            {
                return new ResponseDto<ProductToReturnDto>()
                {
                    IsSuccessful = true,
                    StatusCode = "00",
                    StatusMessage = "Product saved successfully"
                };
            }
            return new ResponseDto<ProductToReturnDto>()
            {
                IsSuccessful = false,
                StatusCode = "08",
                StatusMessage = "Product failed to save. Please try again"
            };
        }
        public async Task<ResponseDto<List<ProductToReturnDto>>> GetAllProducts()
        {
            var productListToReturn= new List<ProductToReturnDto>();
            var productsList = await _dbContext.Products.ToListAsync();
            if (productsList.Count>0)
            {
                foreach (var product in productsList)
                {

                    var productToReturn = new ProductToReturnDto()
                    {
                        Category = product.Category,
                        Name = product.Name,
                        Description = product.Description,
                        Id = product.Id
                    };
                    productListToReturn.Add(productToReturn);
                }
                return new ResponseDto<List<ProductToReturnDto>>()
                {
                    Data = productListToReturn,
                    IsSuccessful = true,
                    StatusCode = "00",
                    StatusMessage = "Category products retrieved"

                };
            }
            return new ResponseDto<List<ProductToReturnDto>>()
            {
                IsSuccessful = false,
                StatusCode = "14",
                StatusMessage = "No product found in the category"

            };
        }
        public async Task<ResponseDto<List<ProductToReturnDto>>> GetProductsByProductCategoryName(string categoryName)
        {
            var category = await _categoryServices.GetCategoryByName(categoryName);
            if(category == null)
            {
                return new ResponseDto<List<ProductToReturnDto>>()
                {
                    IsSuccessful = false,
                    StatusCode = "14",
                    StatusMessage = $"No category found with the name {categoryName}"
                };
            }
            var productList = await GetProductByCategoryName(categoryName);
            if(productList!=null)
            {
                return new ResponseDto<List<ProductToReturnDto>>()
                {
                    Data = productList,
                    IsSuccessful = true,
                    StatusCode = "00",
                    StatusMessage = "Category products retrieved"

                };
            }
            return new ResponseDto<List<ProductToReturnDto>>()
            {
                IsSuccessful = false,
                StatusCode = "14",
                StatusMessage = "No product found in the category"

            };
        }
        public async Task<ResponseDto<List<ProductToReturnDto>>> GetProductsByProductCategoryId(string categoryId)
        {
            var category = await _categoryServices.GetCategoryById(categoryId);
            if(category == null)
            {
                return new ResponseDto<List<ProductToReturnDto>>()
                {
                    IsSuccessful = false,
                    StatusCode = "14",
                    StatusMessage = $"No category found"
                };
            }
            var productList = await GetProductByCategoryId(categoryId);
            if(productList!=null)
            {
                return new ResponseDto<List<ProductToReturnDto>>()
                {
                    Data = productList,
                    IsSuccessful = true,
                    StatusCode = "00",
                    StatusMessage = "Category products retrieved"

                };
            }
            return new ResponseDto<List<ProductToReturnDto>>()
            {
                IsSuccessful = false,
                StatusCode = "14",
                StatusMessage = "No product found in the category"

            };
        }
        public async Task<ResponseDto<ProductToReturnDto>> GetProductByProductId(string Id)
        {
            var productGet = await GetProductById(Id);
            if (productGet != null)
            {
                var productToReturn = new ProductToReturnDto()
                {
                    Category = productGet.Category,
                    Name = productGet.Name,
                    Description = productGet.Description,
                    Id = productGet.Id
                };
                return new ResponseDto<ProductToReturnDto>()
                {
                    Data = productToReturn,
                    IsSuccessful = true,
                    StatusCode = "00",
                    StatusMessage = "Product Details retrieved"

                };
            }
            return null;
        }
        public async Task<ResponseDto<ProductToReturnDto>> GetProductByProductName(string name)
        {
            var productGet = await GetProductByName(name);
            if (productGet != null)
            {
                var productToReturn = new ProductToReturnDto()
                {
                    Category = productGet.Category,
                    Name = productGet.Name,
                    Description = productGet.Description,
                    Id = productGet.Id
                };
                return new ResponseDto<ProductToReturnDto>()
                {
                    Data = productToReturn,
                    IsSuccessful = true,
                    StatusCode = "00",
                    StatusMessage = "Product Details retrieved"

                };
            }
            return new ResponseDto<ProductToReturnDto>()
            {
                IsSuccessful = false,
                StatusCode = "14",
                StatusMessage = "Product not found"

            };
        }

        public async Task<Product>GetProductByName(string name)
        {
            var productGet = await _dbContext.Products.Where(c => c.Name == name).FirstOrDefaultAsync();
            if (productGet != null)
            {
                
                return productGet;
            }
            return null;
        }
        public async Task<Product>GetProductById(string id)
        {
            var productGet = await _dbContext.Products.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (productGet != null)
            {               
                return productGet;
            }
            return null;
        }
        private async Task<List<ProductToReturnDto>>GetProductByCategoryName(string categoryName)
        {
            var productListToReturn = new List<ProductToReturnDto>();
            var categoryProducts = await _dbContext.Products.Where(c => c.Category.Name == categoryName).ToListAsync();
            
            if (categoryProducts.Count>0)
            {
                foreach(var categoryProduct in categoryProducts)
                {

                    var productToReturn = new ProductToReturnDto()
                    {
                        Category = categoryProduct.Category,
                        Name = categoryProduct.Name,
                        Description = categoryProduct.Description,
                        Id = categoryProduct.Id
                    };

                    productListToReturn.Add(productToReturn);
                }
                
                return productListToReturn;
            }
            return null;
        }private async Task<List<ProductToReturnDto>>GetProductByCategoryId(string categoryId)
        {
            var productListToReturn = new List<ProductToReturnDto>();
            var categoryProducts = await _dbContext.Products.Where(c => c.Category.Id == categoryId).ToListAsync();
            
            if (categoryProducts.Count>0)
            {
                foreach(var categoryProduct in categoryProducts)
                {

                    var productToReturn = new ProductToReturnDto()
                    {
                        Category = categoryProduct.Category,
                        Name = categoryProduct.Name,
                        Description = categoryProduct.Description,
                        Id = categoryProduct.Id
                    };

                    productListToReturn.Add(productToReturn);
                }
                
                return productListToReturn;
            }
            return null;
        }
    }
}
