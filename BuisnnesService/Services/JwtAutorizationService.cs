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
namespace BuisnnesService.Services
{
    public class JwtAutorizationService
    {
        private readonly TokenInfo _options;
        public JwtAutorizationService(IOptionsSnapshot<TokenInfo> options)
            => _options = options.Value;
        public (string, string) CreateToken(User user)
        {
            List<Claim> claims = new() {
                new Claim("FullName",user.FullName),
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
                expires: DateTime.UtcNow.AddMinutes(_options.REFRESH_LIFETIME),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256)
                );
            return (
                new JwtSecurityTokenHandler().WriteToken(jwt),
                new JwtSecurityTokenHandler().WriteToken(jwtRefresh)
                );
        }
        //public (string, string) CreateToken(string token)
        //{
        //    var JWTReader = new JwtSecurityTokenHandler();
        //    var  parametrs = JWTReader.ReadJwtToken(token).Payload.Values;
        //    List<Claim> claims = new() {
        //        new Claim("FullName" ,parametrs.Single(x=>x).ToString()),
        //        new Claim("Id",parametrs["Id"].ToString())
        //        };
        //    var SecretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.KEY));
        //    var jwt = new JwtSecurityToken(
        //        issuer: _options.ISSUER,
        //        audience: _options.AUDIENCE,
        //        claims: claims,
        //        expires: DateTime.Now.AddMinutes(_options.LIFETIME),
        //        notBefore: DateTime.Now,
        //        signingCredentials: new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256)
        //        );
        //    var jwtRefresh = new JwtSecurityToken(
        //        issuer: _options.ISSUER,
        //        audience: _options.AUDIENCE_REFRASH,
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddMinutes(_options.REFRESH_LIFETIME),
        //        notBefore: DateTime.UtcNow,
        //        signingCredentials: new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256)
        //        );
        //    return (
        //        new JwtSecurityTokenHandler().WriteToken(jwt),
        //        new JwtSecurityTokenHandler().WriteToken(jwtRefresh)
        //        );

        //}
        public bool IsValid(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            SecurityToken validatedToken;
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);
            var id = principal.FindFirstValue("FullName");
            var claim = principal.Claims.First();
            var v = validatedToken;

            return true;
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.KEY))
            };
        }
    }
}
