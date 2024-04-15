using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.NoteRepository
{
    public class NoteProcedureRepository : INoteRepository
    {
        private readonly TaskTrackerDbContext _context;
        private readonly ILogger<NoteRepository> _logger;
        public NoteProcedureRepository(TaskTrackerDbContext context, ILogger<NoteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Note> CreateAsync(Note note)
        {
            await _context.Create_Note(note);
            var entity = await _context.Notes.Where(x => x.UserId == note.UserId).OrderBy(x=>x.Id).LastAsync();
            _logger.LogDebug($"Note added, id = {entity.Id}");
            return entity;
        }

        public async Task DeleteAsync(long Id)
        {
            var note = await _context.Notes.SingleAsync(x => x.Id == Id);
            if (note.UserId != UserClaims.User.Id)
                throw new ArgumentException("Access denied");
            await _context.Delete_Note(Id);
            _logger.LogDebug($"Note deleted, id = {Id}");
        }

        public async Task<Note> GetNoteAsync(long id)
        {
            var entity = await _context.Notes.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            var entities = await _context.Notes.AsNoTracking().ToListAsync();
            return entities;
        }

        public async Task<Note> UpdateAsync(Note note)
        {
            if (note.UserId != UserClaims.User.Id)
                throw new ArgumentException("Access denied");
            await _context.Update_Note(note);
            _logger.LogDebug($"Note changed, id = {note.Id}");
            return await _context.Notes.SingleAsync(x => x.Id == note.Id);
        }
    }
}
