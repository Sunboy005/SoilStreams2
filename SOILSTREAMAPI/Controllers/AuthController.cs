using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SOILSTREAMAPI.Data;
using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Models.Dto;
using SOILSTREAMAPI.Services.Interfaces;

namespace SOILSTREAMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly SoilStreamsDbContext _dbContext;

        public AuthController(SoilStreamsDbContext dbContext, UserManager<User> userManager, SignInManager<User> signInManager, IAuthServices authServices)
        {
            _userManager = userManager;
            _signInManager=signInManager;
            _dbContext=dbContext;
        }


       //public async Task<IActionResult>Login(AuthRequest model)
       // {
       //     var user= _userManager.GetUserAsync()
       //     var login=await _signInManager.SignInAsync()
       // }
    }
}
