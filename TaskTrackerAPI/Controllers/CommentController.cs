using BuisnnesService.Commands.Comment.Create;
using BuisnnesService.Commands.Comment.Delete;
using BuisnnesService.Commands.Comment.Update;
using BuisnnesService.Queries.Comment.GetCommentsForTask;
using BuisnnesService.Queries.Tasks;
using Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaskTrackerAPI.Controllers
{
    public class CommentController : MyBaseController
    {
        private readonly IMediator _mediator;
        public CommentController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet("{TaskId}")]
        public async Task<List<Comment>> Get(long TaskId)
            => await _mediator.Send(new GetCommentsForTaskQuery() { Id = TaskId });

        [HttpDelete("{Id}")]
        public async Task Delete(long Id)
             => await _mediator.Send(new DeleteCommentCommand() { Id = Id });

        [HttpPost]
        public async Task<Comment> Create(CreateCommentCommand command)
            => await _mediator.Send(command);

        [HttpPut]
        public async Task<Comment> Update(UpdateCommentCommand command)
            => await _mediator.Send(command);


    }
}
