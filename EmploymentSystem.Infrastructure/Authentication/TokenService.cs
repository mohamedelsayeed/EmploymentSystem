using EmploymentSystem.Application.Absctractions;
using EmploymentSystem.Application.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmploymentSystem.Persistance.Authentication
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string CreateToken(IdentityUser user)
        {
            var signingCredintials = new SigningCredentials(
                              new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                              SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim (JwtRegisteredClaimNames.Sub,user.Id.ToString()),
                new Claim (JwtRegisteredClaimNames.Email,user.Email.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
            };
            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audiance,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                signingCredentials: signingCredintials);

            var tokenStr = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenStr;
        }
    }
}
