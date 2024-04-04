using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SOILSTREAMAPI.Common;
using SOILSTREAMAPI.Data;
using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Models.Dto;
using SOILSTREAMAPI.Services.Interfaces;

namespace SOILSTREAMAPI.Services.Implementations
{
    public class OrderServices : IOrderServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SoilStreamsDbContext _dbContext;

        public OrderServices(UserManager<User> userManager, SoilStreamsDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        public async Task<ResponseDto<OrderToReturnDto>> CreateOrder(OrderToCreate model)
        {
            //check the user/consumer
            var user = await _userManager.FindByEmailAsync(model.OrderBy);
            if (user == null)
            {
                return new ResponseDto<OrderToReturnDto>()
                {
                    IsSuccessful = false,
                    StatusCode = "24",
                    StatusMessage = "User not registered. Please sign up to order"
                };
            }
            var trackingId = UtilsServices.GenerateTrackingId();
            var orderToSave = new Order()
            {
                ConsumerUser = user,
                DeliveryAddress = model.DeliveryAddress,
                OrderedProducts = model.OrderItems,
                DateCreated = DateTime.Now,
                Status = false,
                TrackingId = trackingId,
                DeliveryStatus = false,
                TotalAmount = model.TotalAmount,
                ConsumerUserId = user.Id,
                OrderDate = DateTime.Now,
                ExpectedDeliveryDate = DateTime.Now.AddDays(3)
            };

            var saveOrder = _dbContext.Orders.Add(orderToSave);
            var saveContext = _dbContext.SaveChanges();
            if (saveContext > 0)
            {
                var OrderToReturn = await GetOrderByTrackingId(orderToSave.TrackingId);
                if (OrderToReturn != null)
                {
                    return new ResponseDto<OrderToReturnDto>()
                    {
                        IsSuccessful = true,
                        StatusCode = "00",
                        StatusMessage = "Order placed successfully",
                        Data = OrderToReturn
                    };
                }
            }
            return new ResponseDto<OrderToReturnDto>()
            {
                IsSuccessful = false,
                StatusCode = "09",
                StatusMessage = "Failed to place order"
            };

        }

        public async Task<ResponseDto<List<OrderToReturnDto>>> GetAllOrders()
        {
            var customerOrders = new List<OrderToReturnDto>();
            var orders = await _dbContext.Orders.ToListAsync();
            if (orders.Count > 0)
            {
                foreach (var order in orders)
                {
                    var orderToreturn = new OrderToReturnDto()
                    {
                        TrackingId = order.TrackingId,
                        OrderDate = order.OrderDate,
                        ExpectedDeliveryDate = order.ExpectedDeliveryDate,
                        OrderId = order.Id,
                        OrderBy = order.ConsumerUser.Id,
                        DeliveryAddress = order.DeliveryAddress
                    };
                    customerOrders.Add(orderToreturn);
                }
            }
            return new ResponseDto<List<OrderToReturnDto>>
            {
                IsSuccessful = false,
                StatusCode = "04",
                StatusMessage = "No order found"
            };
        }

        public async Task<OrderToReturnDto> GetOrderByTrackingId(string trackingId)
        {
            var orderDetails = await _dbContext.Orders.Where(c => c.TrackingId == trackingId).FirstOrDefaultAsync();
            if (orderDetails != null)
            {
                return new OrderToReturnDto()
                {
                    TrackingId = orderDetails.TrackingId,
                    OrderDate = orderDetails.OrderDate,
                    ExpectedDeliveryDate = orderDetails.ExpectedDeliveryDate,
                    OrderId = orderDetails.Id,
                    OrderBy = orderDetails.ConsumerUser.Id,
                    DeliveryAddress = orderDetails.DeliveryAddress
                };
            }
            return null;
        }

        public async Task<OrderToReturnDto> GetOrderByOrderId(string orderId)
        {
            var orderDetails = await _dbContext.Orders.Where(c => c.Id == orderId).FirstOrDefaultAsync();
            if (orderDetails != null)
            {
                return new OrderToReturnDto()
                {
                    TrackingId = orderDetails.TrackingId,
                    OrderDate = orderDetails.OrderDate,
                    ExpectedDeliveryDate = orderDetails.ExpectedDeliveryDate,
                    OrderId = orderDetails.Id,
                    OrderBy = orderDetails.ConsumerUser.Id,
                    DeliveryAddress = orderDetails.DeliveryAddress
                };
            }
            return null;
        }

        public async Task<ResponseDto<List<OrderToReturnDto>>> GetOrderByUserId(string userId)
        {
            var customerOrders = new List<OrderToReturnDto>();
            var user = await _userManager.FindByIdAsync(userId);
            var orderDetails = await _dbContext.Orders.Where(c => c.ConsumerUser == user).ToListAsync();
            if (orderDetails.Count > 0)
            {
                foreach (var order in orderDetails)
                {
                    var orderToreturn = new OrderToReturnDto()
                    {
                        TrackingId = order.TrackingId,
                        OrderDate = order.OrderDate,
                        ExpectedDeliveryDate = order.ExpectedDeliveryDate,
                        OrderId = order.Id,
                        OrderBy = order.ConsumerUser.Id,
                        DeliveryAddress = order.DeliveryAddress
                    };
                    customerOrders.Add(orderToreturn);
                }
            }
            return new ResponseDto<List<OrderToReturnDto>>
            {
                IsSuccessful = false,
                StatusCode = "04",
                StatusMessage = "No order found"
            };
        }

        public async Task<ResponseDto<OrderToReturnDto>> UpdateOrderStatus(OrderToUpdate model)
        {
            var order = await _dbContext.Orders.Where(o => o.Id == model.OrderId).FirstOrDefaultAsync();
            if (order == null)
            {
                return new ResponseDto<OrderToReturnDto>
                {
                    IsSuccessful = false,
                    StatusMessage = "Order Not found",
                    StatusCode = "24"
                };
            }
            var orderToEdit = new Order()
            {
                DeliveryStatus = model.DeliveryStatus,
                DeliveryAddress = model.DeliveryAddress,
                Status = model.Status,
                ExpectedDeliveryDate = model.ExpectedDeliveryDate
            };
            _dbContext.Orders.Update(orderToEdit);
            var saveUpdate = _dbContext.SaveChanges();
            if (saveUpdate > 0)
            {
                var orderToReturn = await GetOrderByTrackingId(order.TrackingId);
                return new ResponseDto<OrderToReturnDto>
                {
                    IsSuccessful = true,
                    StatusCode = "00",
                    StatusMessage = "Order updated successfully",
                    Data = orderToReturn

                };
            }
            return new ResponseDto<OrderToReturnDto>
            {
                IsSuccessful = false,
                StatusCode = "09",
                StatusMessage = "Failed to update orders"
            };

        }
    }
}
