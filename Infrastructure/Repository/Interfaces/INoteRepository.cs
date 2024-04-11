using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface INoteRepository
    {
        public Task<Note> CreateAsync(Note note);
        public Task<Note> GetNoteAsync(long id);
        public Task<List<Note>> GetNotesAsync();
        public Task<Note> UpdateAsync(Note note);
        public Task DeleteAsync(Note note);
    }
}
