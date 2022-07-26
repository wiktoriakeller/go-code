﻿using GoCode.Application.Common.Services;
using GoCode.Infrastructure.Identity.Dto;
using GoCode.Infrastructure.Identity.Entities;
using GoCode.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoCode.Infrastructure.Identity
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly UserManager<User> _userManager;
        private readonly IDateTimeProvider _dateTimeProvider;

        public JwtService(
            IOptions<JwtOptions> jwtOptions,
            TokenValidationParameters tokenValidationParameters,
            UserManager<User> userManager,
            IDateTimeProvider dateTimeProvider)
        {
            _jwtOptions = jwtOptions.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _userManager = userManager;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<JwtTokenInfoDto> CreateJwtToken(User user)
        {
            var (claims, jti) = await GetJwtTokenClaims(user);

            var key = Encoding.UTF8.GetBytes(_jwtOptions.Key);
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _dateTimeProvider.UtcNow,
                expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes),
                signingCredentials: signingCredentials);

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return new JwtTokenInfoDto
            {
                Token = jwtToken,
                Jti = jti
            };
        }

        public ClaimsPrincipal? GetPrincipalFromJwtToken(string jwtToken, bool validateLifetime)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = _tokenValidationParameters.ValidateIssuerSigningKey,
                    ValidIssuer = _tokenValidationParameters.ValidIssuer,
                    ValidAudience = _tokenValidationParameters.ValidAudience,
                    IssuerSigningKey = _tokenValidationParameters.IssuerSigningKey,
                    ValidateLifetime = validateLifetime ? true : false,
                    ClockSkew = _tokenValidationParameters.ClockSkew,
                };

                var principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out var validatedToken);
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

        private async Task<(IEnumerable<Claim> claims, string jti)> GetJwtTokenClaims(User user)
        {
            var jti = Guid.NewGuid().ToString();
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, jti),
                new Claim(JwtRegisteredClaimNames.Iss, _jwtOptions.Issuer),
                new Claim(JwtRegisteredClaimNames.Aud, _jwtOptions.Audience),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);

            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return (claims, jti);
        }
    }
}
