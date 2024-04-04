namespace SOILSTREAMAPI.Models.Dto
{
    public class AuthResponse
    {
        public string JWTToken { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
