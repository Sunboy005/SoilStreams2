using SOILSTREAMAPI.Models.Dto;

namespace SOILSTREAMAPI.Services.Interfaces
{
    public interface IAuthServices
    {
        Task<ResponseDto<AuthResponse>> LoginUser(AuthRequest model);
        //Task<ResponseDto<AuthResponse>> ForgotPassword(string userName);
    }
}
