using Microsoft.AspNetCore.Identity;
using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Models.Dto;
using SOILSTREAMAPI.Services.Interfaces;

namespace SOILSTREAMAPI.Services.Implementations
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJWTService _jWTService;
        public AuthServices(UserManager<User> userManager, SignInManager<User> signInManager, IJWTService jWTService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jWTService = jWTService;
        }
        //Task<ResponseDto<AuthResponse>> IAuthServices.ForgotPassword(string userName)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ResponseDto<AuthResponse>> LoginUser(AuthRequest model)
        {
            //Checking if user exist in the system
            var user = await _userManager.FindByEmailAsync(model.Username);
            if (user == null)
            {
                return new ResponseDto<AuthResponse>()
                {
                    StatusMessage = "Invalid User. Please sign up.",
                    StatusCode = "04",
                    IsSuccessful = false
                };
            }

            var signIn = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
            if (!signIn.Succeeded)
            {
                return new ResponseDto<AuthResponse>()
                {

                };
            }
            var userRoles = await _userManager.GetRolesAsync(user);
            var token = _jWTService.GenerateJWTToken(user, userRoles);
            var authResponse = new AuthResponse()
            {
                JWTToken = token,
                UserRoles = userRoles,
            };
            return new ResponseDto<AuthResponse>()
            {
                Data = authResponse,
                IsSuccessful = true,
                StatusCode = "00",
                StatusMessage = "Login Successful"
            };

        }
    }
}
