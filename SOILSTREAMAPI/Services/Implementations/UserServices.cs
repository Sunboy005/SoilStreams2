using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SOILSTREAMAPI.Data;
using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Models.Dto;
using SOILSTREAMAPI.Services.Interfaces;

namespace SOILSTREAMAPI.Services.Implementations
{
    public class UserServices : IUserServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SoilStreamsDbContext _dbContext;

        public UserServices(SoilStreamsDbContext dbContext, UserManager<User> userManager)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }
        public async Task<ResponseDto<UserMinInfoDto>> CreateUser(UserRegistrationDto model)
        {
            var userExist = await _userManager.FindByEmailAsync(model.Email);
            if (userExist != null)
            {
                return new ResponseDto<UserMinInfoDto>()
                {
                    StatusMessage = "User already exist in the system.",
                    StatusCode = "05",
                    IsSuccessful = false
                };
            }
            var userToCreate = new User()
            {
                Address = model.Address,
                Email = model.Email,
                UserName = model.Email,
                FullName = model.FullName,
                PhoneNo = model.PhoneNumber,
                Location = model.Location,
                DateModified = DateTime.UtcNow,
                DateCreated = DateTime.UtcNow,
            };

            var createUser = await _userManager.CreateAsync(userToCreate, model.Password);
            if (!createUser.Succeeded)
                return new ResponseDto<UserMinInfoDto>()
                {
                    StatusMessage = "Error occurred while creating the user. Please try again",
                    StatusCode = "07",
                    IsSuccessful = false
                };
            return new ResponseDto<UserMinInfoDto>()
            {
                StatusMessage = "User Created Successfully. Please Login.",
                StatusCode = "00",
                IsSuccessful = true
            };

        }

        public Task<ResponseDto<UserMinInfoDto>> EditUser(UserDetailsToEditDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<List<UserDetailsDto>>> GetAllAgents()
        {
            try
            {
                // Get RoleId
                var agentRole = await _dbContext.Roles.Where(x => x.Name == "Agent").FirstOrDefaultAsync();
                var agentRoleId = agentRole.Id;
                var agentsIdList = await _dbContext.UserRoles.Where(r => r.RoleId == agentRoleId).ToListAsync();

                List<UserDetailsDto> userList = new List<UserDetailsDto>();
                //loop Through the UserList to get the details
                foreach (var agent in agentsIdList)
                {
                    var agentToReturn = await GetUserById(agentRoleId);
                    userList.Add(agentToReturn);
                }
                if (userList.Count > 0)
                {
                    return new ResponseDto<List<UserDetailsDto>>()
                    {
                        StatusMessage = "User data retrieved.",
                        StatusCode = "00",
                        IsSuccessful = true,
                        Data = userList
                    };
                }
                return new ResponseDto<List<UserDetailsDto>>()
                {
                    StatusMessage = "No agent found.",
                    StatusCode = "04",
                    IsSuccessful = true,
                    Data = userList
                };
            }
            catch (Exception)
            {
                return new ResponseDto<List<UserDetailsDto>>()
                {
                    StatusMessage = "Error retrieving list of agent.",
                    StatusCode = "99",
                    IsSuccessful = false,
                    Data = null
                };
            }

        }

        public Task<ResponseDto<List<UserDetailsDto>>> GetAllConsumers()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto<List<UserDetailsDto>>> GetAllFarmers()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto<List<UserDetailsDto>>> GetAllUser()
        {
            try
            {
                var userList= new List<UserDetailsDto>();
                var users = await _userManager.Users.ToListAsync();
                foreach (var user in users)
                {
                    var userToReturn = await GetUserById(user.Id);
                    userList.Add(userToReturn);
                }
                if (userList.Count > 0)
                {
                    return new ResponseDto<List<UserDetailsDto>>()
                    {
                        StatusMessage = "User data retrieved.",
                        StatusCode = "00",
                        IsSuccessful = true,
                        Data = userList
                    };
                }
                return new ResponseDto<List<UserDetailsDto>>()
                {
                    StatusMessage = "No user found.",
                    StatusCode = "04",
                    IsSuccessful = true,
                    Data = userList
                };
            }
            catch (Exception)
            {
                return new ResponseDto<List<UserDetailsDto>>()
                {
                    StatusMessage = "Error retrieving list of users.",
                    StatusCode = "99",
                    IsSuccessful = false,
                    Data = null
                };
            }

        }

        public async Task<ResponseDto<UserDetailsDto>> GetUserByEmail(string email)
        {
            //Use the user Email to get the user ID
            var user= await  _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var userToReturn= await GetUserById(user.Id);
                return new ResponseDto<UserDetailsDto>()
                {
                    StatusMessage = "User data retrieved.",
                    StatusCode = "00",
                    IsSuccessful = true,
                    Data = userToReturn
                };
            }

            return new ResponseDto<UserDetailsDto>()
            {
                StatusMessage = "No user with the email found.",
                StatusCode = "04",
                IsSuccessful = false
            };

        }

        public async Task<UserDetailsDto> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var rolesToReturn = new List<string>();
            if (user != null)
            {
                //Get Role
                var userRole = await _dbContext.UserRoles.Where(x => x.UserId == id).FirstOrDefaultAsync();
                //var role = GetRoleByID(userRole.RoleId);
                var roles = await _userManager.GetRolesAsync(user);
                //rolesToReturn.Add(role);
                //Get UserProduct
                var userProducts = await _dbContext.StoreProducts.Where(s => s.User == user).ToListAsync();
                //Get User Orders
                var userOrders = await _dbContext.Orders.Where(s => s.ConsumerUser == user).ToListAsync();

                var userToReturn = new UserDetailsDto()
                {
                    Address = user.Address,
                    DateCreated = user.DateCreated,
                    FullName = user.FullName,
                    PhoneNo = user.PhoneNo,
                    Location = user.Location,
                    Id = user.Id,
                    Role = (List<string>)roles,
                    //Role = rolesToReturn,
                    Orders = userOrders,
                    StoreProducts = userProducts,
                    DateModified = user.DateModified
                };
                return userToReturn;
            }
            return null;
        }

        private string GetRoleByID(string id)
        {
            var role = _dbContext.Roles.FirstOrDefault(x => x.Id == id);
            return role.Name;
        }

        public Task<ResponseDto<UserMinInfoDto>> GetUserMinInfoById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
