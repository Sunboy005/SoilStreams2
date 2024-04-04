using SOILSTREAMAPI.Models.Dto;

namespace SOILSTREAMAPI.Services.Interfaces
{
    public interface IUserServices
    {
        Task<UserDetailsDto>GetUserById(string id);
        Task<ResponseDto<UserDetailsDto>>GetUserByEmail(string email);
        Task<ResponseDto<List<UserDetailsDto>>>GetAllUser();
        Task<ResponseDto<List<UserDetailsDto>>>GetAllFarmers();
        Task<ResponseDto<List<UserDetailsDto>>>GetAllAgents();
        Task<ResponseDto<List<UserDetailsDto>>>GetAllConsumers();
        Task<ResponseDto<UserMinInfoDto>>GetUserMinInfoById(string id);
        Task<ResponseDto<UserMinInfoDto>>CreateUser(UserRegistrationDto model);
        Task<ResponseDto<UserMinInfoDto>>EditUser(UserDetailsToEditDto model);
    }
}
