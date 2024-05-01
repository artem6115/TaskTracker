using Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Interfaces
{
    public interface IProjectRepository
    {
        public Task<List<Project>> GetParticipateProjectsAsync();
        public Task<List<Project>> GetMyProjectsAsync();
        public Task<Project> GetProjectAsync(long Id);
        public Task ChangeProjectTeam(long projectId,List<long> UsersId);
        public Task<List<User>> GetUsers(long projectId);


        public Task<Project> CreateProjectAsync(Project project);
        public Task<Project> UpdateProjectAsync(Project project);
        public Task DeleteProjectAsync(long Id);

    }
}
