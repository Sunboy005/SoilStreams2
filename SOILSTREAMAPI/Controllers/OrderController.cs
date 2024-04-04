using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOILSTREAMAPI.Data;
using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Models.Dto;
using SOILSTREAMAPI.Services.Implementations;
using SOILSTREAMAPI.Services.Interfaces;

namespace SOILSTREAMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly SoilStreamsDbContext _dbContext;
        private readonly IOrderServices _orderServices;

        public OrderController(SoilStreamsDbContext dbContext, IOrderServices orderServices)
        {
            _dbContext = dbContext;
            _orderServices= orderServices;
        }

        [HttpPost("createorders")]
        [Authorize(Roles ="Consumer")]
        public async Task<IActionResult> CreateOrder(OrderToCreate model)
        {
            if (!ModelState.IsValid)
            {
                var response = new ResponseDto<string>()
                {
                    IsSuccessful = false,
                    StatusCode = "01",
                    StatusMessage = "Invalid input values"
                };
                return NotFound(response);
            }
            var createOrder = await _orderServices.CreateOrder(model);
            if (createOrder.IsSuccessful)
            {
                return Ok(createOrder);
            }
            return BadRequest(createOrder);
        }
        [HttpPost("getallorders")]
        [Authorize(Roles ="Consumer")]
        public async Task<IActionResult> GetAllOrders()
        {
            if (!ModelState.IsValid)
            {
                var response = new ResponseDto<string>()
                {
                    IsSuccessful = false,
                    StatusCode = "01",
                    StatusMessage = "Invalid input values"
                };
                return BadRequest(response);
            }
            var orders = await _orderServices.GetAllOrders();
            if (orders.IsSuccessful)
            {
                return Ok(orders);
            }
            return BadRequest(orders);
        }
    }
}
