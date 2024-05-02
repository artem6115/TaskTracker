using BuisnnesService.Commands.Projects.Create;
using BuisnnesService.Commands.Projects.Delete;
using BuisnnesService.Commands.Projects.Update;
using BuisnnesService.Commands.Projects.UpdateUsers;
using BuisnnesService.Models;
using BuisnnesService.Queries.Project.GetAll;
using BuisnnesService.Queries.Project.GetParticipateProjects;
using BuisnnesService.Queries.Project.GetProject;
using BuisnnesService.Queries.Project.GetUsers;
using Infrastructure.Auth;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskTrackerAPI.Controllers
{
    public class ProjectController : MyBaseController
    {
        private readonly IMediator _mediator;
        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<ProjectDto>> GetParticipateProjects()
            =>await _mediator.Send(new GetParticipateProjectsQuery());

        [HttpGet("My")]
        public async Task<List<ProjectDto>> GetMyProjects()
            => await _mediator.Send(new GetMyProjectsQuery());

        [HttpGet("{Id}")]
        public async Task<ProjectDetails> GetProject([Required] long Id)
            => await _mediator.Send(new GetProjectQuery() { Id = Id});

        [HttpPost]
        public async Task<ProjectDto> CreateProject(CreateProjectCommand command)
             => await _mediator.Send(command);

        [HttpPut]
        public async Task<ProjectDto> UpdateProject(UpdateProjectCommand command)
            => await _mediator.Send(command);

        [HttpPut("Users")]
        public async Task UpdateProjectUsers(UpdateUsersProjectsCommand command)
            => await _mediator.Send(command);
        [HttpGet("{Id}/Users")]
        public async Task<List<UserClaims>> GetProjectUsers([Required]long Id)
                => await _mediator.Send(new GetProjectUsersQuery() { ProjectId=Id});

        [HttpDelete("{Id}")]
        public async Task DeleteProject([Required] long Id)
            => await _mediator.Send(new DeleteProjectCommand() { Id = Id});
    }
}
