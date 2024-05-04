using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Services
{
    public static class EpicService
    {
        public static async Task<List<Epic>> GetProjectEpics(long Id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Epic/Project/{Id}");
            List<Epic> epics = await AuthService.SendAsync<List<Epic>>(request);
            return epics;
        }
        public static async Task<Epic> GetEpic(long Id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Epic/{Id}");
            Epic epic = await AuthService.SendAsync<Epic>(request);
            return epic;
        }
        public static async Task<Epic> CreateEpic(Epic epic)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{LocalConnectionService.Adress}/api/Epic");
            request.Content = JsonContent.Create(new
            {
                epic.Title,
                epic.Description,
                ProjectId = epic.Project.Id
            });
            Epic newEpic = await AuthService.SendAsync<Epic>(request);
            return newEpic;
        }
        public static async Task<Epic> UpdateEpic(Epic epic)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://{LocalConnectionService.Adress}/api/Epic");
            request.Content = JsonContent.Create(new
            {
                epic.Id,
                epic.Title,
                epic.Description,
                ProjectId = epic.Project.Id
            });
            Epic newEpic = await AuthService.SendAsync<Epic>(request);
            return newEpic;
        }
        public static async Task<bool> DeleteEpic(long Id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"https://{LocalConnectionService.Adress}/api/Epic/{Id}");
            var result = await AuthService.SendAsync(request);
            return result is not null && result.IsSuccessStatusCode;
        }

    }
}
