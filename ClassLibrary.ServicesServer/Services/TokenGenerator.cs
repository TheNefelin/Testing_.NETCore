using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClassLibrary.Models.DTOs;
using ClassLibrary.ServicesServer.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace ClassLibrary.ServicesServer.Services
{
    public class TokenGenerator : ITokenGenerator
    {
        public TokenDTO GenerateToken(ApiAuthDTO auth)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>() {
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new(JwtRegisteredClaimNames.Sub, auth.UserId.ToString()),
                new(JwtRegisteredClaimNames.Email, auth.Email),
            };

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(auth.ExpiresMin),
                Issuer = auth.Issuer,
                Audience = auth.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(auth.Key)), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenDTO()
            {
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
