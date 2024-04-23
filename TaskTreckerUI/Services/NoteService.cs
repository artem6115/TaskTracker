using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;
using System.Net.Http.Json;
using TaskTrackerUI.ViewModels;

namespace TaskTrackerUI.Services
{
    public static class NoteService
    {
        public async static Task<List<Note>> GetNotesAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Note");
            List<Note> notes = await AuthService.SendAsync<List<Note>>(request);
            if (notes is null) return null!;
            return notes.OrderByDescending(x=>x.DateOfCreated).ToList();
        }
        public async static Task<Note> GetNoteAsync(long id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Note/{id}");
            Note note = await AuthService.SendAsync<Note>(request);
            return note;
        }
        public async static Task<Note> CreateNoteAsync(Note note)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{LocalConnectionService.Adress}/api/Note");
            request.Content = JsonContent.Create<Note>(note);
            Note newNote = await AuthService.SendAsync<Note>(request);
            return newNote;
        }
        public async static Task<Note> UpdateNoteAsync(Note note)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://{LocalConnectionService.Adress}/api/Note");
            request.Content = JsonContent.Create<Note>(note);
            Note newNote = await AuthService.SendAsync<Note>(request);
            return newNote;
        }
        public async static Task<bool> DeleteNoteAsync(Note note)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"https://{LocalConnectionService.Adress}/api/Note/{note.Id}");
            var result = await AuthService.SendAsync(request);
            if(result == null || !result.IsSuccessStatusCode)return false;
            return true;
        }
       
    }
}
