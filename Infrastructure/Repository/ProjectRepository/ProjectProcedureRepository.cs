using Infrastructure.Utilits;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.ProjectRepository
{
    public class ProjectProcedureRepository : IProjectRepository
    {
        private readonly ILogger<ProjectProcedureRepository> _logger;
        private readonly TaskTrackerDbContext _context;
        public ProjectProcedureRepository(TaskTrackerDbContext context, ILogger<ProjectProcedureRepository> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task<List<User>> GetUsers(long projectId)
            => await _context.UsersProjects
            .AsNoTracking()
            .Where(x => x.ProjectId == projectId)
            .Select(x => x.User)
            .ToListAsync();

        public async Task ChangeProjectTeam(long projectId, List<long> usersId)
        {
            var entities = await _context.UsersProjects
                .AsNoTracking()
                .Where(x => x.ProjectId == projectId)
                .ToListAsync();
            var entityToDelete = entities.ExceptBy(usersId, x => x.UserId);
            var entityToAdd = usersId.Except(entities
                .Select(x=>x.UserId))
                .Select(x=> new UserProject
                    { UserId=x,ProjectId=projectId}
                );
            await _context.AddUserProject(entityToAdd);
            await _context.RemoveUserProject(entityToDelete);


        }

        public async Task<Project> CreateProjectAsync(Project project)
        {
            await _context.Create_Project(project);
            var entity = await _context.Projects
                .Where(x=>x.AuthorId==UserClaims.User.Id)
                .OrderByDescending(x=>x.Id)
                .FirstAsync();
            _logger.LogDebug($"Создан проект, наименоание {entity.Name}, id - {entity.Id}");
            return entity;
        }

        public async Task DeleteProjectAsync(long Id)
        {
            var entity = await _context.Projects.FindAsync(Id);
            if (entity == null)
                throw new FileNotFoundException("Проект не найден");
            if (entity.AuthorId != UserClaims.User.Id)
                throw new AccessViolationException("Вы не можете удалить чужой проект");
            await _context.Delete_Project(Id);
            await _context.SaveChangesAsync();
            _logger.LogDebug($"Удален проект, наименоание {entity.Name}, id - {entity.Id}");
        }

        public async Task<List<Project>> GetMyProjectsAsync()
        {
            var entities = await _context.Projects
                .AsNoTracking()
                .Where(x => x.AuthorId == UserClaims.User.Id)
                .ToListAsync();
            return entities;
        }

        public async Task<List<Project>> GetParticipateProjectsAsync()
        {
            var ProjectsId = await _context.UsersProjects
                .Where(x => x.UserId == UserClaims.User.Id)
                .AsNoTracking()
                .Select(x => x.ProjectId)
                .ToListAsync();
            var entities = await _context.Projects
                .Where(x => ProjectsId.Contains(x.Id))
                .AsNoTracking()
                .ToListAsync();
            return entities;
        }

        public async Task<Project> GetProjectAsync(long Id)
        {
            var entity = await _context.Projects.FindAsync(Id);
            if (entity == null)
                throw new FileNotFoundException("Проект не найден");
            return entity;
        }

        public async Task<Project> UpdateProjectAsync(Project project)
        {
            await _context.Update_Project(project);
            var updateProj =await  _context.Projects.AsNoTracking().SingleAsync(x => x.Id == project.Id);
            _logger.LogDebug($"Project updated, id - {updateProj.Id}, description - {updateProj.Name}");
            return updateProj;

        }
    }
}
