using BuisnnesService.Commands.Notifies.Delete;
using BuisnnesService.Commands.Notifies.Update;
using BuisnnesService.Queries.Notifies;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace TaskTrackerAPI.Controllers
{
    public class NotifyController : MyBaseController
    {
        private readonly IMediator _mediator;
        public NotifyController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public async Task<List<Notify>> Get()
            => await _mediator.Send(new GetAllNotifiesQuery());

        [HttpGet("ReadAll")]
        public async Task ReadAll()
            => await _mediator.Send(new ReadAllNotifiesCommand());

        [HttpGet("DeleteAll")]
        public async Task DeleteAll()
            => await _mediator.Send(new DeleteAllNotifiesCommand());

        [HttpDelete("{Id}")]
        public async Task Delete(long Id)
            => await _mediator.Send(new DeleteNotifyCommand() { Id=Id});
    }
}
