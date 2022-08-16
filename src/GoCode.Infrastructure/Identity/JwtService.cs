using GoCode.Infrastructure.Identity.Entities;
using GoCode.Infrastructure.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoCode.Infrastructure.Identity
{
    internal class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;

        public JwtService(IOptions<JwtOptions> jwtOptions,
            TokenValidationParameters tokenValidationParameters)
        {
            _jwtOptions = jwtOptions.Value;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public (string jwtToken, string jti) CreateJwtToken(User user)
        {
            var (claims, jti) = GetJwtTokenClaims(user);

            var key = Encoding.UTF8.GetBytes(_jwtOptions.Key);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes),
                signingCredentials: signingCredentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return (jwtToken, jti);
        }

        public ClaimsPrincipal? GetPrincipalFromJwtToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var principal = tokenHandler.ValidateToken(jwtToken, _tokenValidationParameters, out var validatedToken);

                if (!IsJwtWithValidSecurityAlgorithm(validatedToken))
                {
                    return null;
                }

                return principal;
            }
            catch
            {
                return null;
            }
        }

        private bool IsJwtWithValidSecurityAlgorithm(SecurityToken validatedToken) =>
            (validatedToken is JwtSecurityToken jwtSecurityToken) &&
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

        private (IEnumerable<Claim> claims, string jti) GetJwtTokenClaims(User user)
        {
            var jti = Guid.NewGuid().ToString();
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, _jwtOptions.Audience),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            return (claims, jti);
        }
    }
}
