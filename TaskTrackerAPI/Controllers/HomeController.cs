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
        [HttpGet("allow")]
        public object GetAllow()
        {


            return "That OK!!!" ;
        }

        [HttpGet("Auth")]
        [Authorize]
        public string GetAuth()
        {
            return "Вы вошли в закрытую область";
        }
    }
}
