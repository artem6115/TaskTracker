using BuisnnesService.Commands.Notes.Create;
using BuisnnesService.Commands.Notes.Delete;
using BuisnnesService.Commands.Notes.Update;
using BuisnnesService.Models;
using BuisnnesService.Queries.Notes;
using Infrastructure.Entities;
using Infrastructure.Repository.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace TaskTrackerAPI.Controllers
{
    [Authorize]
    public class NotesController : MyBaseController
    {
        private readonly INoteRepository _noteRepository;

        private readonly IMediator _mediator;
        public NotesController(INoteRepository repository, IMediator mediator)
        {
            _noteRepository = repository; 
            _mediator = mediator;
        }

        [HttpGet] 
        public async Task<List<NoteDto>> Get()
        {
            return await _mediator.Send(new GetNotesQuery());
        }

        [HttpGet("{Id}")]
        public async Task<NoteDto> Get(long id)
        {
            return await _mediator.Send(new GetNoteQuery() { Id = id }) ;
        }
        [HttpPost]
        public async Task<NoteDto> Create([FromBody]NoteCreateCommand command)
        {
            return await _mediator.Send(command);

        }
        [HttpPut]
        public async Task<NoteDto> Update([FromBody] NoteUpdateCommand command)
        {
            return await _mediator.Send(command);
        }
        [HttpDelete("{Id}")]
        public async Task Delete(long id)
        {
           await _mediator.Send(new NoteDeleteCommand() { Id = id }) ;
        }
    }
}
