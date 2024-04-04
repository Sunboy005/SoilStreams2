using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SOILSTREAMAPI.Data;
using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Models.Dto;
using SOILSTREAMAPI.Services.Interfaces;

namespace SOILSTREAMAPI.Services.Implementations
{
    public class StoreProductServices : IStoreProductServices
    {
        public readonly IProductServices _productServices;
        public readonly ICategoryServices _categoryServices;
        public readonly SoilStreamsDbContext _dbContext;
        public readonly UserManager<User> _userManager;

        //public readonly SignInManager<User> _signManager;
        public StoreProductServices(SoilStreamsDbContext dbContext, UserManager<User> userManager, IProductServices productServices, ICategoryServices categoryServices)
        {
            _userManager = userManager;
            _productServices = productServices;
            _categoryServices = categoryServices;
            _dbContext = dbContext;
        }

        public async Task<ResponseDto<StoreProductToReturnDto>> CreateStoreProduct(StoreProductCreationDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return new ResponseDto<StoreProductToReturnDto>()
                {
                    IsSuccessful = false,
                    StatusCode = "08",
                    StatusMessage = "Invalid User, You cannot create"
                };
            }
            
            var product = await _productServices.GetProductById(model.ProductId);
            if (product == null)
            {
                return new ResponseDto<StoreProductToReturnDto>()
                {
                    IsSuccessful = false,
                    StatusCode = "06",
                    StatusMessage = "Invalid product selected"
                };
            }
            var storeProduct = new StoreProduct()
            {
                AvailableQuantity = model.AvailableQuantity,
                Product = product,
                ProductId = model.ProductId,
                UnitPrice = model.UnitPrice,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                User = user,
                UserId = model.UserId
            };
            _dbContext.StoreProducts.Add(storeProduct);
            var saveStoreProduct = _dbContext.SaveChanges();
            if (saveStoreProduct > 0)
            {
                return new ResponseDto<StoreProductToReturnDto>()
                {
                    IsSuccessful = true,
                    StatusCode = "00",
                    StatusMessage = "Store product saved successfully."
                };
            }
            return new ResponseDto<StoreProductToReturnDto>()
            {
                IsSuccessful = false,
                StatusCode = "08",
                StatusMessage = "Store product failed to add in the store"
            };
        }

        public async Task<ResponseDto<List<StoreProductToReturnDto>>> GetAllStoreProducts()
        {
            var storeProductListToReturn = new List<StoreProductToReturnDto>();
            var storeProductsList = await _dbContext.StoreProducts.Where(x => x.IsDeleted == false).ToListAsync();
            if (storeProductsList.Count > 0)
            {
                foreach (var product in storeProductsList)
                {
                    //var category=_categoryServices.GetCategoryByCategoryId(product.Product.Category.Id)
                    var productToReturn = new StoreProductToReturnDto()
                    {
                        Category = product.Product.Category,
                        StoreProductName = product.StoreProductName,
                        AvailableQuantity = product.AvailableQuantity,
                        Product = product.Product,
                        Id = product.Id,
                        UnitPrice = product.UnitPrice
                    };
                    storeProductListToReturn.Add(productToReturn);
                }
                return new ResponseDto<List<StoreProductToReturnDto>>()
                {
                    Data = storeProductListToReturn,
                    IsSuccessful = true,
                    StatusCode = "00",
                    StatusMessage = "Store products retrieved successfully"
                };
            }
            return new ResponseDto<List<StoreProductToReturnDto>>()
            {
                IsSuccessful = false,
                StatusCode = "14",
                StatusMessage = "No product found in the store"

            };
        }

        public async Task<ResponseDto<List<StoreProductToReturnDto>>> GetStoreProductsByCategory(string categoryId)
        {
            var category = await _categoryServices.GetCategoryById(categoryId);
            if (category == null)
            {
                return new ResponseDto<List<StoreProductToReturnDto>>()
                {
                    StatusCode = "08",
                    StatusMessage = "Invalid category",
                    IsSuccessful = false,
                };
            }
            var productList = new List<StoreProductToReturnDto>();
            var storeProducts = await _dbContext.StoreProducts.Where(t => t.Product.Category == category && t.IsDeleted == false).ToListAsync();
            if (storeProducts.Count > 0)
            {
                foreach (var storeProduct in storeProducts)
                {
                    var product = new StoreProductToReturnDto()
                    {
                        Product = storeProduct.Product,
                        Category = category,
                        AvailableQuantity = storeProduct.AvailableQuantity,
                        Id = storeProduct.Id,
                        StoreProductName = storeProduct.StoreProductName,
                        UnitPrice = storeProduct.UnitPrice
                    };
                    productList.Add(product);
                }
                return new ResponseDto<List<StoreProductToReturnDto>>()
                {
                    StatusCode = "00",
                    StatusMessage = "Successfully fetch the store products",
                    IsSuccessful = true,
                    Data = productList
                };
            }
            return new ResponseDto<List<StoreProductToReturnDto>>()
            {
                IsSuccessful = false,
                StatusCode = "14",
                StatusMessage = "No product found"
            };
        }

        public async Task<ResponseDto<List<StoreProductToReturnDto>>> GetStoreProductsByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return new ResponseDto<List<StoreProductToReturnDto>>()
                {
                    StatusCode = "04",
                    StatusMessage = "Invalid user",
                    IsSuccessful = false,
                };
            }
            var productList = new List<StoreProductToReturnDto>();
            var storeProducts = await _dbContext.StoreProducts.Where(t => t.User == user && t.IsDeleted == false).ToListAsync();
            if (storeProducts.Count > 0)
            {
                foreach (var storeProduct in storeProducts)
                {
                    var product = new StoreProductToReturnDto()
                    {
                        Product = storeProduct.Product,
                        Category = storeProduct.Product.Category,
                        AvailableQuantity = storeProduct.AvailableQuantity,
                        Id = storeProduct.Id,
                        StoreProductName = storeProduct.StoreProductName,
                        UnitPrice = storeProduct.UnitPrice
                    };
                    productList.Add(product);
                }
                return new ResponseDto<List<StoreProductToReturnDto>>()
                {
                    StatusCode = "00",
                    StatusMessage = "Successfully fetch the store products",
                    IsSuccessful = true,
                    Data = productList
                };
            }
            return new ResponseDto<List<StoreProductToReturnDto>>()
            {
                IsSuccessful = false,
                StatusCode = "14",
                StatusMessage = "No product found"

            };
        }
        public async Task<ResponseDto<bool>> DeleteStoreProducts(string userId, string storeProductId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var productList = new List<StoreProductToReturnDto>();
            var storeProduct = await _dbContext.StoreProducts.Where(t => t.User == user && t.Id == storeProductId).FirstOrDefaultAsync();
            if (storeProduct != null)
            {
                storeProduct.IsDeleted = true;
                _dbContext.StoreProducts.Update(storeProduct);
                return new ResponseDto<bool>()
                {
                    StatusCode = "00",
                    StatusMessage = "Successfully deleted store product",
                    IsSuccessful = true,
                    Data = true
                };
            }
            return new ResponseDto<bool>()
            {
                IsSuccessful = false,
                StatusCode = "14",
                StatusMessage = "Fail to delete store products"

            };
        }
    }
}
