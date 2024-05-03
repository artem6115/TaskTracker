using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TaskTrackerUI.Models;

namespace TaskTrackerUI.Services
{
    public static class ProjectService
    {
        #region Get
        public static async Task<Project> GetProject(long Id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Project/{Id}");
            var project = await AuthService.SendAsync<Project>(request);
            return project;
        }
        public static async Task<List<User>> GetUsers(long Id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Project/{Id}/Users");
            var users = await AuthService.SendAsync<List<User>>(request);
            return users;
        }
        public static async Task<List<ProjectDto>> GetMyProjects()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Project/My");
            var projects = await AuthService.SendAsync<List<ProjectDto>>(request);
            return projects;
        }
        public static async Task<List<ProjectDto>> GetMyParticipateProjects()
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, $"https://{LocalConnectionService.Adress}/api/Project");
            var projects = await AuthService.SendAsync<List<ProjectDto>>(request);
            return projects;
        }
        #endregion

        public static async Task<ProjectDto> CreateProject(Project model)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"https://{LocalConnectionService.Adress}/api/Project");
            request.Content =JsonContent.Create<Project>(model);
            var project = await AuthService.SendAsync<ProjectDto>(request);
            if(project is not null)
                await ChangeProjectTeam(project.Id,model.Users);
            return project;
        }
        public static async Task<ProjectDto> UpdateProject(Project model)
        {
            model.AuthorId = model.Author.Id;
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://{LocalConnectionService.Adress}/api/Project");
            request.Content = JsonContent.Create<Project>(model);
            var project = await AuthService.SendAsync<ProjectDto>(request);
            if (project is not null)
                await ChangeProjectTeam(model.Id, model.Users);
            return project;
        }
        public static async Task<bool> DeleteProject(long Id)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, $"https://{LocalConnectionService.Adress}/api/Project/{Id}");
            var result = await AuthService.SendAsync(request);
            return result is not null && result.IsSuccessStatusCode;
        }
        public static async Task<bool> ChangeProjectTeam(ChangeProjectTeamQuery query)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, $"https://{LocalConnectionService.Adress}/api/Project/Users");
            request.Content = JsonContent.Create<ChangeProjectTeamQuery>(query);
            var result = await AuthService.SendAsync(request);
            return result is not null && result.IsSuccessStatusCode;
        }
        public static async Task<bool> ChangeProjectTeam(long projectId, IEnumerable<User> UsersId)
            => await ChangeProjectTeam(
                new ChangeProjectTeamQuery()
                {
                    ProjectId = projectId,
                    UsersId = UsersId.Select(x=>x.Id)
                }
            );


    }
}
