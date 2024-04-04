using Microsoft.IdentityModel.Tokens;
using SOILSTREAMAPI.Models;
using SOILSTREAMAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SOILSTREAMAPI.Services.Implementations
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _config;
        public JWTService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public string GenerateJWTToken(User user, IEnumerable<string> userRoles)
        {
            //Adding user claims
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, $"{user.FullName}"),
                new Claim(ClaimTypes.Email, user.Email),
            };
            foreach (var role in userRoles)
                Claims.Add(new Claim(ClaimTypes.Role, role));
            //Set up system security
            var SymmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(Claims),
                Expires = DateTime.Today.AddDays(1),
                SigningCredentials = new SigningCredentials(SymmetricSecurity, SecurityAlgorithms.HmacSha256)
            };
            //Create token
            var SecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = SecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return SecurityTokenHandler.WriteToken(token);
        }
    }
}
