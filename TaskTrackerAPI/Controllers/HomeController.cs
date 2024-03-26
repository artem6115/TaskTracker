using BuisnnesService.Services;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TaskTrackerAPI.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly JwtAutorizationService _service;
        public HomeController(JwtAutorizationService service)
        {
            _service = service;
        }
        [HttpGet("allow")]
        public object GetAllow()
        {
            return "That OK!!!" ;
        }

        [HttpGet("Login")]
        public object GetAllow(string FullName,string Email,string Password)
        {
            var user = new User() { Email = Email, Password = Password , FullName = FullName};
            var tokens = _service.CreateToken(user);
            user.RefreshToken = tokens.Item2;
            return new { Token = tokens.Item1, RefreshToken = tokens.Item2 };
        }
        [HttpGet("Refresh")]
        public object Check(string token)
        {
            return _service.IsValid(token);
        }

        [HttpGet("Auth")]
        [Authorize]
        public string GetAuth()
        {
            return "Вы вошли в закрытую область";
        }
    }
}

