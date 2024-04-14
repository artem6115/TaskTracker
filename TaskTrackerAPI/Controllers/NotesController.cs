using Infrastructure.Entities;
using Infrastructure.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TaskTrackerAPI.Controllers
{
    [Authorize]
    public class NotesController : MyBaseController
    {
        private readonly INoteRepository _noteRepository;
        public NotesController(INoteRepository repository)
        {
            _noteRepository = repository; 
        }

        [HttpGet] 
        public async Task<List<Note>> Get()
        {
            return await _noteRepository.GetNotesAsync();
        }

        [HttpGet("{id}")]
        public async Task<Note> Get([FromQuery]int id)
        {
            return await _noteRepository.GetNoteAsync(id);
        }
        [HttpPost]
        public async Task<Note> Create([FromBody]Note note)
        {
            return await _noteRepository.CreateAsync(note);
        }
        [HttpPut]
        public async Task<Note> Update([FromBody] Note note)
        {
            return await _noteRepository.UpdateAsync(note);
        }
        [HttpDelete]
        public async Task Delete([FromBody] Note note)
        {
            await _noteRepository.DeleteAsync(note);
        }
    }
}
