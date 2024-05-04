using BuisnnesService.Commands.Epics.Create;
using BuisnnesService.Commands.Epics.Delete;
using BuisnnesService.Commands.Epics.Update;
using BuisnnesService.Models;
using BuisnnesService.Queries.Epics;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskTrackerAPI.Controllers
{
    public class EpicController : MyBaseController
    {
        private readonly IMediator _mediator;
        public EpicController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{Id}")]
        public async Task<EpicDto> GetEpic([Required] long Id)
            => await _mediator.Send(new GetEpicQuery() { Id = Id });

        [HttpGet("Project/{ProjectId}")]
        public async Task<List<EpicDto>> GetProjectEpics([Required] long ProjectId)
            => await _mediator.Send(new GetProjectEpicsQuery() { Id = ProjectId });


        [HttpPost]
        public async Task<EpicDto> Create(CreateEpicCommand command)
            => await _mediator.Send(command);

        [HttpPut]
        public async Task<EpicDto> Update(UpdateEpicCommand command)
            => await _mediator.Send(command);

        [HttpDelete("{Id}")]
        public async Task Delete([Required]long Id)
            => await _mediator.Send(new DeleteEpicCommand() { Id=Id});
    }
}
