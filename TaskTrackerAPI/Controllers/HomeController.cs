using BuisnnesService.Services;
using Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TaskTrackerAPI.Controllers
{
    [AllowAnonymous]
    public class HomeController : MyBaseController
    {

        [HttpGet("/")]
        public object Get() => Ok();
        

    }
}

