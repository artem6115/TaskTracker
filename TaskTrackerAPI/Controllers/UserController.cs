using AutoMapper;
using BuisnnesService.Services;
using Infrastructure.Auth;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace TaskTrackerAPI.Controllers
{
    [AllowAnonymous]
    public class UserController : MyBaseController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository rep,IMapper mapper)
        {
            _userRepository = rep;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<List<UserClaims>> FindUsers([NotNull][FromBody] string EmailPattern)
        {
            var result = await _userRepository.FindUsers(EmailPattern);
            return _mapper.Map<List<UserClaims>>(result);
        }

        [HttpGet("GenerateUser/{Code}")]
        public async Task GenerateUser(int code)
        {
            if (code != 2122) throw new AccessViolationException();
            for(int i = 0; i<= 500; i++)
            {
                var user = new User()
                {
                    Email = Guid.NewGuid().ToString(),
                    FullName = "Generated Name",
                    Password = "Password",
                };
                await _userRepository.AddUserAcync(user);
            }

        }
        
    }
}
