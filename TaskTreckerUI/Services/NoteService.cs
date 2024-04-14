using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;
using System.Net.Http.Json;

namespace TaskTrackerUI.Services
{
    public static class NoteService
    {
        public async static Task<List<Note>> GetNotesAsync()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Notes");
            var result = await AuthService.SendAsync(request) ;
            if(result == null)
                return null!;
            List<Note> notes = await result.Content.ReadFromJsonAsync<List<Note>>();
            return notes;
        }
        public async static Task<Note> GetNoteAsync(long id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Notes/{id}");
            var result = await AuthService.SendAsync(request);
            if (result == null)
                return null!;
            Note note = await result.Content.ReadFromJsonAsync<Note>();
            return note;
        }
        public async static Task<Note> CreateNoteAsync(Note note)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{LocalConnectionService.Adress}/api/Notes");
            request.Content = JsonContent.Create<Note>(note);
            var result = await AuthService.SendAsync(request);
            if (result == null)
                return null!;
            Note newNote = await result.Content.ReadFromJsonAsync<Note>();
            return newNote;
        }
        public async static Task<Note> UpdateNoteAsync(Note note)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://{LocalConnectionService.Adress}/api/Notes");
            request.Content = JsonContent.Create<Note>(note);

            var result = await AuthService.SendAsync(request);
            if (result == null)
                return null!;
            Note newNote = await result.Content.ReadFromJsonAsync<Note>();
            return newNote;
        }
        public async static Task DeleteNoteAsync(Note note)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"https://{LocalConnectionService.Adress}/api/Notes");
            request.Content = JsonContent.Create<Note>(note);

            var result = await AuthService.SendAsync(request);

        }
    }
}
