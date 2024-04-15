using AutoMapper;
using AutoMapper.Features;
using BuisnnesService.Commands.Notes.Delete;
using BuisnnesService.Models;
using Infrastructure.Auth;
using Infrastructure.Entities;
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
            CreateMap<UserClaims, User>().ReverseMap();

            CreateMap<NoteDto, Note>().ReverseMap();

            //CreateMap<NoteDto,Note>().ForMember(dto => dto.User ,
            //    cnf => cnf.MapFrom(x=> UserClaims.User));
            CreateMap<NoteCreateCommand, Note>().ForMember(dto => dto.DateOfCreated,
                cnf => cnf.MapFrom(x => DateTime.Now))
                .ForMember(dto => dto.UserId,
                cnf => cnf.MapFrom(x => UserClaims.User.Id));

            CreateMap<NoteUpdateCommand, Note>()
                .ForMember(dto => dto.DateOfChanged,
                cnf => cnf.MapFrom(x => DateTime.Now))
                .ForMember(dto => dto.UserId,
                cnf => cnf.MapFrom(x => UserClaims.User.Id));
            CreateMap<NoteDeleteCommand, Note>();








        }
    }
}
