using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOILSTREAMAPI.Data;
using SOILSTREAMAPI.Models;

namespace SOILSTREAMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly SoilStreamsDbContext _dbContext;

        public OrderController(SoilStreamsDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
