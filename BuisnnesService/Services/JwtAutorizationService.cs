using Infrastructure.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using JWT;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BuisnnesService.Models;
namespace BuisnnesService.Services
{
    public class JwtAutorizationService
    {
        private readonly TokenInfo _options;
        public JwtAutorizationService(IOptionsSnapshot<TokenInfo> options)
            => _options = options.Value;
        public (string, string) CreateToken(UserClaims user)
        {
            List<Claim> claims = new() {
                new Claim("FullName",user.FullName),
                new Claim("Email",user.Email),
                new Claim("Id",user.Id.ToString())
                };
            var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.KEY));
            var jwt = new JwtSecurityToken(
                issuer: _options.ISSUER,
                audience: _options.AUDIENCE,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_options.LIFETIME),
                notBefore: DateTime.Now,
                signingCredentials: new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256)
                );
            var jwtRefresh = new JwtSecurityToken(
                issuer: _options.ISSUER,
                audience: _options.AUDIENCE_REFRASH,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(_options.REFRESH_LIFETIME),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256)
                );
            return (
                new JwtSecurityTokenHandler().WriteToken(jwt),
                new JwtSecurityTokenHandler().WriteToken(jwtRefresh)
                );
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = _options.ISSUER,
                ValidAudience = _options.AUDIENCE_REFRASH,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.KEY)),
                ClockSkew = TimeSpan.Zero,
                
            };
        }
        public UserClaims DesirializeToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            return new UserClaims()
            {
                Id = long.Parse(principal.FindFirstValue("Id")),
                FullName = principal.FindFirstValue("FullName"),
                Email = principal.FindFirstValue("Email"),

            };
        }
    }
}
