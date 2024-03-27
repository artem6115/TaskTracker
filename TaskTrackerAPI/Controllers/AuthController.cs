using BuisnnesService.Models;
using BuisnnesService.Services;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace TaskTrackerAPI.Controllers
{
    public class AuthController : MyBaseController
    {
        private readonly UserManagerService _userManager;
        public AuthController(UserManagerService service)
        {
            _userManager = service;
        }

        [HttpGet("Login")]
        public async Task<IActionResult> Login([FromQuery] UserDto user)
        { 
            var result = await _userManager.LoginAsync(user);
            if (result.Success) return Ok(result);
            else return NotFound(result.ErrorMessage);
        }

        [Authorize]
        [HttpGet("Logout")]
        public async Task Logout()
            => await _userManager.LogoutAsync();

        [HttpGet("Regist")]
        public async Task<IActionResult> Regist([FromQuery]UserDto user)
        { 
            var result = await _userManager.RegistAsync(user);
            if (result.Success) return Ok(result);
            else return BadRequest(result.ErrorMessage);
        }

        [HttpGet("RefreshToken")]
        public async Task<IActionResult> Refresh(string token)
        {
            var result = await _userManager.RefreshAsync(token);
            if (result.Success) return Ok(result);
            else return Unauthorized(result.ErrorMessage);
            
        }

    }
}
