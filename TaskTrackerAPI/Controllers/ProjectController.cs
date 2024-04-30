using BuisnnesService.Commands.Projects.Create;
using BuisnnesService.Commands.Projects.Delete;
using BuisnnesService.Commands.Projects.Update;
using BuisnnesService.Models;
using BuisnnesService.Queries.Project.GetAll;
using BuisnnesService.Queries.Project.GetParticipateProjects;
using BuisnnesService.Queries.Project.GetProject;
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
        public async Task<List<ProjectDto>> GetParticipateProjects(GetParticipateProjectsQuery query)
            =>await _mediator.Send(query);

        [HttpGet("/My")]
        public async Task<List<ProjectDto>> GetMyProjects(GetMyProjectsQuery query)
            => await _mediator.Send(query);

        [HttpGet("{Id}")]
        public async Task<Project> GetProject([Required] long Id)
            => await _mediator.Send(new GetProjectQuery() { Id = Id});

        [HttpPost]
        public async Task<ProjectDto> CreateProject(CreateProjectCommand command)
             => await _mediator.Send(command);

        [HttpPut]
        public async Task<ProjectDto> UpdateProject(UpdateProjectCommand command)
            => await _mediator.Send(command);

        [HttpDelete("{Id}")]
        public async Task DeleteProject([Required] long Id)
            => await _mediator.Send(new DeleteProjectCommand() { Id = Id});
    }
}
