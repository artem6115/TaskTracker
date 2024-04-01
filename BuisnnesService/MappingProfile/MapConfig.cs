using AutoMapper;
using BuisnnesService.Models;
using Infrastructure.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuisnnesService.MappingProfile
{
    public class MapConfig : Profile
    {
        public MapConfig() { 
            CreateMap<UserRegistDto,User>().ReverseMap();
            CreateMap<UserClaims, UserRegistDto>().ReverseMap();
            CreateMap<UserLoginDto, User>().ReverseMap();
            CreateMap<UserClaims, UserLoginDto>().ReverseMap();

        }
    }
}
