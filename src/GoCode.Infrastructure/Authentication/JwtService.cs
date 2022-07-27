using GoCode.Application.Contracts;
using GoCode.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoCode.Infrastructure.Authentication
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public string CreateJwtToken(User user)
        {
            var claims = GetTokenClaims(user);

            var key = Encoding.UTF8.GetBytes(_jwtOptions.Key);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes),
                signingCredentials: signingCredentials);

            var jwtTokenHandler = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtTokenHandler;
        }

        private IEnumerable<Claim> GetTokenClaims(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, _jwtOptions.Audience),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Surname, user.Surname),
                new Claim(ClaimTypes.Email, user.Email)
            };

            return claims;
        }
    }
}