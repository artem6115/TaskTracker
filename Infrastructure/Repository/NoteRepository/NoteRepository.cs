using Infrastructure.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.NoteRepository
{
    public class NoteRepository : INoteRepository
    {
        private readonly TaskTrackerDbContext _context;
        private readonly ILogger<NoteRepository> _logger;
        public NoteRepository(TaskTrackerDbContext context, ILogger<NoteRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<Note> CreateAsync(Note note)
        {
            var entity = await _context.AddAsync(note);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Note added, id = {entity.Entity.Id}");
            return entity.Entity;
        }

        public async Task DeleteAsync(long Id)
        {
            var note = await _context.Notes.AsNoTracking().SingleAsync(x => x.Id == Id );
            if (note.UserId != UserClaims.User.Id)
                throw new ArgumentException("Access denied");
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Note deleted, id = {Id}");
        }

        public async Task<Note> GetNoteAsync(long id)
        {
            var entity = await _context.Notes.AsNoTracking().SingleAsync(x => x.Id == id );
            if (entity.UserId != UserClaims.User.Id)
                throw new ArgumentException("Access denied");
            return entity;
        }

        public async Task<List<Note>> GetNotesAsync()
        {
            var entities = await _context.Notes
                .AsNoTracking()
                .Where(x=>x.UserId == UserClaims.User.Id)
                .ToListAsync();
            return entities;
        }

        public async Task<Note> UpdateAsync(Note note)
        {
            if (note.UserId != UserClaims.User.Id)
                throw new ArgumentException("Access denied");
            var newNote = _context.Notes.Update(note);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Note changed, id = {newNote.Entity.Id}");
            return newNote.Entity;
        }
    }
}
