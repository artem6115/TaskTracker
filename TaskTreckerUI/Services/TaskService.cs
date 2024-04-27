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
    public static class TaskService
    {
        public static async Task<List<TaskDto>> GetMyTasksAsync() {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Task");
            List<TaskDto> tasks = await AuthService.SendAsync<List<TaskDto>>(request);
            return tasks;
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

        public async static Task<List<TaskDto>> GetTasksInEpic(long epicId)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Task/epic/{epicId}");
            List<TaskDto> tasks = await AuthService.SendAsync<List<TaskDto>>(request);
            return tasks;
        }

        public async static Task<TaskDto> CreateTaskAsync(TaskInfo task)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{LocalConnectionService.Adress}/api/Task");
            request.Content = JsonContent.Create(new {
                task.Title,
                task.Description,
                task.ApproximateDateOfCompleted,
                task.Importance,
                PreviousTaskId = task.PreviousTask?.Id,
                UserId = task.User?.Id
                });
            TaskDto resultTask = await AuthService.SendAsync<TaskDto>(request);
            return resultTask;
        }

        public async static Task<TaskDto> UpdateTaskAsync(TaskInfo task)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://{LocalConnectionService.Adress}/api/Task");
            request.Content = JsonContent.Create(new
            {
                task.Id,
                task.Title,
                task.Description,
                task.ApproximateDateOfCompleted,
                task.Importance,
                task.StatusTask,
                PreviousTaskId = task.PreviousTask?.Id,
                UserId = task.User?.Id
            });
            TaskDto resultTask = await AuthService.SendAsync<TaskDto>(request);
            return resultTask;
        }

        public async static Task<TaskDto> SetStatusAsync(long Id, Models.TaskStatus Status)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://{LocalConnectionService.Adress}/api/Task/status");
            request.Content = JsonContent.Create(new
            {
                Id,
                StatusTask = Status
            });
            TaskDto resultTask = await AuthService.SendAsync<TaskDto>(request);
            return resultTask;
        }
    }
}
