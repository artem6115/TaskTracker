using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskTrackerAPI.Auth
{
    public static class AuthOptions
    {
        public static void Options(JwtBearerOptions opt,ConfigurationManager Configuration) {
            opt.RequireHttpsMetadata = false;
            opt.TokenValidationParameters = new TokenValidationParameters
            {
                // укзывает, будет ли валидироваться издатель при валидации токена
                ValidateIssuer = true,
                // строка, представляющая издателя
                ValidIssuer = Configuration["Auth:ISSUER"],

                // будет ли валидироваться потребитель токена
                ValidateAudience = true,
                // установка потребителя токена
                ValidAudience = Configuration["Auth:AUDIENCE"],
                // будет ли валидироваться время существования
                ValidateLifetime = true,

                // установка ключа безопасности
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Auth:KEY"]??"DEFOULT_KEY3456рекуце45унк6г7елгншбоьр")),
                // валидация ключа безопасности
                ValidateIssuerSigningKey = true,

                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
