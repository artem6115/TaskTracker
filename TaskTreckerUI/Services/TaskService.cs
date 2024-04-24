using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Services
{
    public static class TaskService
    {
        public static async Task<List<TaskDto>> GetMyTasksAsync() {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Task");
            List<TaskDto> tasks = await AuthService.SendAsync<List<TaskDto>>(request);
            if (tasks is null) return null!;
            return tasks.OrderByDescending(x => x.ApproximateDateOfCompleted).ToList();
        }
        public static async Task<TaskInfo> GetTaskAsync(long Id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Task/{Id}");
            var task = await AuthService.SendAsync<TaskInfo>(request);
            return task;
        }

        public async static Task<bool> DeleteTaskAsync(TaskDto task)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"https://{LocalConnectionService.Adress}/api/Task/{task.Id}");
            var result = await AuthService.SendAsync(request);
            if (result is null || !result.IsSuccessStatusCode) return false;
            return true;
        }
        
    }
}
