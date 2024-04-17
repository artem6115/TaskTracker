using BuisnnesService.Commands.Tasks.Create;
using BuisnnesService.Commands.Tasks.Delete;
using BuisnnesService.Commands.Tasks.Update;
using BuisnnesService.Models;
using BuisnnesService.Queries.Tasks;
using Infrastructure.Repository.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskTrackerAPI.Controllers
{
    public class TaskController : MyBaseController
    {

        private readonly IMediator _mediator;
        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<List<TaskView>> Get()
        {
            return await _mediator.Send(new GetMyTasksQuery());
        }

        [HttpGet("{id}")]
        public async Task<TaskDto> Get([Required] long id)
        {
            return await _mediator.Send(new GetTaskQuery() {Id=id });
        }

        [HttpGet("epic/{id}")]
        public async Task<List<TaskView>> GetForEpic([Required] long id)
        {
            return await _mediator.Send(new GetTasksForEpicQuery() { Id = id });
        }

        [HttpPost]
        public async Task<TaskView> Add(CreateTaskCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut]
        public async Task<TaskView> Update(UpdateTaskCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpDelete("{Id}")]
        public async Task<bool> Delete([Required] long id)
        {
            return await _mediator.Send(new DeleteTaskCommand() { Id = id });
        }
    }
}
