using BuisnnesService.Models;
using BuisnnesService.Services;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using TaskTrackerAPI.Validators;

namespace TaskTrackerAPI.Controllers
{
    public class AuthController : MyBaseController
    {
        private readonly UserManagerService _userManager;
        public AuthController(UserManagerService service)
        {
            _userManager = service;
        }

        [HttpGet("GetUser")]
        [Authorize]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userManager.GetUser();
            if (user is null) return NotFound();
            return Ok(user);    
        }

        [HttpPut("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto user)
        {
            var result = await _userManager.LoginAsync(user);
            if (result.Success) return Ok(result);
            else return NotFound(result.ErrorMessage);
        }

        [Authorize]
        [HttpGet("Logout")]
        public async Task Logout()
            => await _userManager.LogoutAsync();

        [HttpPost("Regist")]
        public async Task<IActionResult> Regist([FromBody] UserRegistDto user)
        {
            if (!ModelState.IsValid) return BadRequest(user);
            var result = await _userManager.RegistAsync(user);
            if (result.Success) return Ok(result);
            else return BadRequest(result.ErrorMessage);
        }

        [HttpPut("RefreshToken")]
        public async Task<IActionResult> Refresh([FromBody] string token)
        {
            var result = await _userManager.RefreshAsync(token);
            if (result.Success) return Ok(result);
            else return Unauthorized(result.ErrorMessage);

        }

        [Authorize]
        [HttpGet("RemovePassword")]
        public async Task<IActionResult> RemovePassword([MinLength(5)][NotNull][Required]string password)
        {
            bool valid = UserLoginDtoValidator.PasswordValidator(password);
            if (!valid) ModelState.AddModelError("errors", "В пароле должна быть как миним 1 буква в верхнем и нижнем регистре");
            if (!ModelState.IsValid) return BadRequest(ModelState);
            await _userManager.RemovePassword(password);
            return Ok();
        }

    }
}
