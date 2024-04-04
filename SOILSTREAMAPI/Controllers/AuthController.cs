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
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthServices _authService;
        private readonly SoilStreamsDbContext _dbContext;

        public AuthController(SoilStreamsDbContext dbContext, UserManager<User> userManager, IAuthServices authServices)
        {
            _userManager = userManager;
            _dbContext=dbContext;
            _authService=authServices;
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthRequest model)
        {
            if (!ModelState.IsValid)
            {
                var response = new ResponseDto<string>()
                {
                    IsSuccessful = false,
                    StatusCode = "01",
                    StatusMessage = "User not registered"
                };
                return NotFound(response);
            }
            var login=await _authService.LoginUser(model);
            if (login.IsSuccessful)
            {
                return Ok(login);
            }
               return BadRequest(login);
         }
    }
}
