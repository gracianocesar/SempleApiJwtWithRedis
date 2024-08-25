using Microsoft.IdentityModel.Tokens;
using SempleApiJwtWithRedis.Application.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SempleApiJwtWithRedis.Application.Services
{
    public static class TokenService
    {
       
        public static string GenerateToken(User user) 
        {
            var key = Encoding.ASCII.GetBytes(Settings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor 
            { 
              Subject = new ClaimsIdentity(new Claim[]
              {
                  new Claim(ClaimTypes.Name, user.Name),
              }),
              Expires = DateTime.UtcNow.AddHours(3),
              SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token =tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

    public static class Settings
    {
        public static string Secret = "7dd138507b633f79c4a95fac0295261ab064c9a83039326ac4c3ebb60541e74f";
    }
}
