using BuisnnesService.Services;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TaskTrackerAPI.Controllers
{
    public class HomeController : MyBaseController
    {

        [HttpGet("All")]
        public  object GetAllow()
        {
            return "Вы в общей области" ;
        }
        [HttpGet("/")]
        public object Get()
        {
            return Ok();
        }

        [HttpGet("Auth")]
        [Authorize]
        public  string GetAuth()
        {
            return "Вы вошли в закрытую область";
        }
    }
}

