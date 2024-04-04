using SOILSTREAMAPI.Models;

namespace SOILSTREAMAPI.Services.Interfaces
{
    public interface IJWTService
    {
        string GenerateJWTToken(User user, IEnumerable<string> userRoles);
    }
}
