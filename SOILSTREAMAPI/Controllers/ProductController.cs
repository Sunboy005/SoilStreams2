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
    public class ProductController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SoilStreamsDbContext _dbContext;

        public ProductController(SoilStreamsDbContext dbContext, UserManager<User> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
    }
}
