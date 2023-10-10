using AutoMapper.Configuration.Conventions;

namespace AGDAR
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        public string jwtIssuer { get; set; }
    }
}
