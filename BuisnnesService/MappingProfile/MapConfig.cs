﻿using BuisnnesService.Commands.Comment.Create;
using BuisnnesService.Commands.Comment.Update;
using BuisnnesService.Commands.Epics.Create;
using BuisnnesService.Commands.Epics.Update;
using BuisnnesService.Commands.Notifies.Create;
using Infrastructure.Entities;
using Microsoft.Identity.Client;
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

            #region User
            CreateMap<UserRegistDto,User>().ReverseMap();
            CreateMap<UserClaims, UserRegistDto>().ReverseMap();
            CreateMap<UserLoginDto, User>().ReverseMap();
            CreateMap<UserClaims, UserLoginDto>().ReverseMap();
            CreateMap<UserClaims, User>().ReverseMap();
            #endregion

            #region Note
            CreateMap<NoteDto, Note>().ReverseMap();
            CreateMap<NoteCreateCommand, Note>().ForMember(dto => dto.DateOfCreated,
                cnf => cnf.MapFrom(x => DateTime.Now))
                .ForMember(dto => dto.UserId,
                cnf => cnf.MapFrom(x => UserClaims.User.Id));

            CreateMap<NoteUpdateCommand, Note>()
                .ForMember(dto => dto.DateOfChanged,
                cnf => cnf.MapFrom(x => DateTime.Now));
            CreateMap<NoteDeleteCommand, Note>();
            #endregion

            #region Task
            CreateMap<CreateTaskCommand, WorkTask>()
                .ForMember(e => e.DateOfCreated, cnf => cnf.MapFrom(x => DateTime.Now))
                .ForMember(e=>e.StatusTask,cnf=>cnf.MapFrom(x=>SetAutoStatus(x)))
                .ForMember(e=>e.UserId,cnf=>cnf.MapFrom(x=> SetAutoUser(x)));
            CreateMap<UpdateTaskCommand, WorkTask>();
            CreateMap<WorkTask, TaskView>();
            //.ForMember(e => e.Epic, cnf => cnf.MapFrom(x => x.Epic));
            CreateMap<WorkTask, TaskDto>();
            CreateMap<UpdateStatusTaskCommand, WorkTask>();

            #endregion

            #region Project

            CreateMap<CreateProjectCommand, Project>()
                .ForMember(e => e.AuthorId, cnf => cnf.MapFrom(x => UserClaims.User.Id ));
            CreateMap<UpdateProjectCommand, Project>();
            CreateMap<Project, ProjectDto>();
            CreateMap<Project, ProjectDetails>();

            #endregion

            #region Epic

            CreateMap<Epic, EpicView>();
            CreateMap<CreateEpicCommand, Epic>();
            CreateMap<UpdateEpicCommand, Epic>();
            CreateMap<Epic,EpicDto>();

            #endregion

            #region Comment

            CreateMap<CreateCommentCommand, Comment>()
                .ForMember(e=>e.Date,cnf=>cnf.MapFrom(x=> DateTime.Now))
                .ForMember(e => e.WorkTaskId, cnf => cnf.MapFrom(x => x.WorkTaskId))
                .ForMember(e=>e.UserId,cnf=>cnf.MapFrom(x=> UserClaims.User.Id));
            CreateMap<UpdateCommentCommand, Comment>()
                .ForMember(e => e.Date, cnf => cnf.MapFrom(x => DateTime.Now));

            #endregion

            #region Notify
            CreateMap<CreateNotifyCommand, Notify>()
                .ForMember(x => x.Date, cnf => cnf.MapFrom(x => DateTime.Now));
            #endregion






        }
        public long? SetAutoUser(CreateTaskCommand command)
        {
            if(command.UserId is not null)return command.UserId;
            if (command.EpicId == null)
                return UserClaims.User.Id;
            return null;
        }
        public Infrastructure.Entities.TaskStatus SetAutoStatus(CreateTaskCommand command) {
            if (command.PreviousTaskId is not null) return Infrastructure.Entities.TaskStatus.Blocked;
            if (command.UserId is not null)
                return Infrastructure.Entities.TaskStatus.Work;
            return Infrastructure.Entities.TaskStatus.Free;
        }

    }
}
